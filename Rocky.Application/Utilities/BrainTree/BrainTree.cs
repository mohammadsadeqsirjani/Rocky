using Braintree;
using Microsoft.Extensions.Options;

namespace Rocky.Application.Utilities.BrainTree
{
    public class BrainTree : IBrainTreeGateway
    {
        private readonly BrainTreeSetting _options;
        private IBraintreeGateway _gateway;


        public BrainTree(IOptions<BrainTreeSetting> options)
        {
            _options = options.Value;
        }
        public IBraintreeGateway CreateGateway()
        {
            return new BraintreeGateway
            {
                Environment = Environment.SANDBOX,
                MerchantId = _options.MerchantId,
                PublicKey = _options.PublicKey,
                PrivateKey = _options.PrivateKey
            };
        }

        public IBraintreeGateway GetGateway()
        {
            return _gateway ??= CreateGateway();
        }
    }
}