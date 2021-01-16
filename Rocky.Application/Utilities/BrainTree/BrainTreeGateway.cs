using Braintree;
using Microsoft.Extensions.Options;

namespace Rocky.Application.Utilities.BrainTree
{
    public class BrainTreeGateway : IBrainTreeGateway
    {
        public BrainTreeSetting BrainTreeSetting { get; set; }
        private IBraintreeGateway _brainTreeGateway;

        public BrainTreeGateway(IOptions<BrainTreeSetting> options)
        {
            BrainTreeSetting = options.Value;
        }

        public IBraintreeGateway CreateGateway()
        {
            return new BraintreeGateway
            {
                Environment = Environment.SANDBOX,
                MerchantId = BrainTreeSetting.MerchantId,
                PublicKey = BrainTreeSetting.PublicKey,
                PrivateKey = BrainTreeSetting.PrivateKey
            };
        }

        public IBraintreeGateway GetGateway()
        {
            return _brainTreeGateway ??= CreateGateway();
        }
    }
}