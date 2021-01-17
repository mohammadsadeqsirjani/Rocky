using Braintree;

namespace Rocky.Application.Utilities.BrainTree
{
    public interface IBrainTreeGateway
    {
        IBraintreeGateway CreateGateway();
        IBraintreeGateway GetGateway();
    }
}
