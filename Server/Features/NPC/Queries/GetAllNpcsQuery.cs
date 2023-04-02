using Kobold.Server.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kobold.Server.Features.NPC.Queries;

public record GetAllNpcsQuery : IRequest<IEnumerable<Shared.Model.NPC>>;

public class Handler : IRequestHandler<GetAllNpcsQuery, IEnumerable<Shared.Model.NPC>>
{
    private readonly ApplicationDbContext _context;

    public Handler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Shared.Model.NPC>> Handle(GetAllNpcsQuery request, CancellationToken cancellationToken)
    {
        return await _context.NPCs.ToListAsync(cancellationToken);
    }
}