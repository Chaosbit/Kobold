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

template = """
Du bist der Assisten des Meister einer DnD 5e Kampagne. Deine Aufgabe ist es, neue NPCs zu erstellen. 
Bei den Namen orientiere dich bitte an gängigen DnD-Namen, benutze nicht den Beruf als Namen. 
Für einen NPC sollst du folgende Informationen generieren:
- Name
- Rasse
- Beruf
- Gesinnung
- Äußerliche Erscheinung (maximal 3 Sätze)
- Charakterliche Eigenschaften (maximal 4 Sätze)

{{user_input}}
"""

async def npc(update: Update, context: ContextTypes.DEFAULT_TYPE):
    user_input = ' '.join(context.args)
    logging.log(logging.DEBUG, user_input)
    prompt = template.replace("{{user_input}}", user_input)
    response = await openai.Completion.acreate(
        engine="text-davinci-003",
        prompt=prompt,
        temperature=0.7,
        max_tokens=350,
        n=1,
        stop=None,
        timeout=10,
    )

    new_npc = response.choices[0].text.strip()
    logging.log(logging.DEBUG, new_npc)
    await context.bot.send_message(chat_id=update.effective_chat.id, text=new_npc)


if __name__ == '__main__':
    application = ApplicationBuilder().token(os.getenv("TELEGRAM_TOKEN")).build()

    start_handler = CommandHandler('npc', npc)
    application.add_handler(start_handler)

    application.run_polling()
