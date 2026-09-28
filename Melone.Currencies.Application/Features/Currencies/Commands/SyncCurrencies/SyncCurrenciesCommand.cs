using MediatR;

namespace Melone.Currencies.Application.Features.Currencies.Commands.SyncCurrencies;

public record SyncCurrenciesCommand : IRequest<DateTime?>;
