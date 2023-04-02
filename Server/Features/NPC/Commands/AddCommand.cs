using AutoMapper;
using Kobold.Server.Data;
using Kobold.Server.Mappings;
using MediatR;

namespace Kobold.Server.Features.NPC.Commands;

public class AddCommand : Shared.Model.NPC, IRequest, IMapFrom<Shared.Model.NPC>
{
    public AddCommand(string firstName, string lastName, string race) : base(firstName, lastName, race)
    {
    }

    public void Mapping(Profile profile) => profile.CreateMap<AddCommand, Shared.Model.NPC>()
        .ForMember(dest => dest.Id, opt => opt.Ignore());
}

public class Handler : IRequestHandler<AddCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public Handler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(AddCommand request, CancellationToken cancellationToken)
    {
        var newNpc = _mapper.Map<Shared.Model.NPC>(request);
        _context.Add(newNpc);
        
        await _context.SaveChangesAsync(cancellationToken);
    }
}