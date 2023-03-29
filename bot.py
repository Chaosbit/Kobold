import logging
from telegram import Update
from dotenv import load_dotenv
import os
import openai
from telegram.ext import ApplicationBuilder, ContextTypes, CommandHandler

load_dotenv()

logging.basicConfig(
    format='%(asctime)s - %(name)s - %(levelname)s - %(message)s',
    level=logging.DEBUG
)

openai.api_key = os.getenv("OPENAI_API_KEY")


async def help(update: Update, context: ContextTypes.DEFAULT_TYPE):
    available_commands = [f'/{file_name.replace(".txt", "")}' for file_name in os.listdir("prompt_templates") if file_name.endswith(".txt")]
    await context.bot.send_message(chat_id=update.effective_chat.id, text="Available commands: " + ', '.join(available_commands))


def handle_template_command(file_name):
    with open(f"prompt_templates/{file_name}.txt", "r") as file:
        template = file.read()
        
        async def handle_command(update: Update, context: ContextTypes.DEFAULT_TYPE):
            user_input = ' '.join(context.args)
            prompt = template.replace("{{user_input}}", user_input)
            response = await openai.Completion.acreate(
                engine="text-davinci-003",
                prompt=prompt,
                temperature=0.7,
                max_tokens=1500,
                n=1,
                stop=None,
                timeout=10,
            )

            gpt_response = response.choices[0].text.strip().replace(".", "\\.").replace("-", "\\-")
            logging.log(logging.DEBUG, gpt_response)
            await context.bot.send_message(chat_id=update.effective_chat.id, text=gpt_response, parse_mode="MarkdownV2")

        return handle_command


if __name__ == '__main__':
    application = ApplicationBuilder().token(os.getenv("TELEGRAM_TOKEN")).build()

    for file_name in os.listdir("prompt_templates"):
        if file_name.endswith(".txt"):
            file_name = file_name.replace(".txt", "")
            template_handler = handle_template_command(file_name)
            handler = CommandHandler(file_name, template_handler)
            application.add_handler(handler)

    handler = CommandHandler('help', help)
    application.add_handler(handler)

    application.run_polling()
