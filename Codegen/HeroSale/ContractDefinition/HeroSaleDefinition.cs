using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System.Numerics;

namespace PirateQuester.HeroSale.ContractDefinition
{


	public partial class HeroSaleDeployment : HeroSaleDeploymentBase
	{
		public HeroSaleDeployment() : base(BYTECODE) { }
		public HeroSaleDeployment(string byteCode) : base(byteCode) { }
	}

	public class HeroSaleDeploymentBase : ContractDeploymentMessage
	{
		public static string BYTECODE = "608060405260405162000f4038038062000f408339810160408190526200002691620004d4565b82816200005560017f360894a13ba1a3210667c828492db98dca3e2076cc3735a920a3ca505d382bbd62000603565b60008051602062000ef9833981519152146200008157634e487b7160e01b600052600160045260246000fd5b6200008f82826000620000ff565b50620000bf905060017fb53127684a568b3173ae13b9f8a6016e243e63b6e8ee1178d6a717850b5d610462000603565b60008051602062000ed983398151915214620000eb57634e487b7160e01b600052600160045260246000fd5b620000f68262000170565b5050506200066c565b6200010a83620001cb565b6040516001600160a01b038416907fbc7cd75a20ee27fd9adebab32041f755214dbc6bffa90cc0225b39da2e5c2d3b90600090a26000825111806200014c5750805b156200016b576200016983836200029360201b6200026c1760201c565b505b505050565b7f7e644d79422f17c01e4894b5f4f588d331ebfa28653d42ae832dc59e38c9798f6200019b620002c2565b604080516001600160a01b03928316815291841660208301520160405180910390a1620001c881620002fb565b50565b620001e1816200038b60201b620002981760201c565b620002495760405162461bcd60e51b815260206004820152602d60248201527f455243313936373a206e657720696d706c656d656e746174696f6e206973206e60448201526c1bdd08184818dbdb9d1c9858dd609a1b60648201526084015b60405180910390fd5b806200027260008051602062000ef983398151915260001b6200039560201b620002141760201c565b80546001600160a01b0319166001600160a01b039290921691909117905550565b6060620002bb838360405180606001604052806027815260200162000f196027913962000398565b9392505050565b6000620002ec60008051602062000ed983398151915260001b6200039560201b620002141760201c565b546001600160a01b0316905090565b6001600160a01b038116620003625760405162461bcd60e51b815260206004820152602660248201527f455243313936373a206e65772061646d696e20697320746865207a65726f206160448201526564647265737360d01b606482015260840162000240565b806200027260008051602062000ed983398151915260001b6200039560201b620002141760201c565b803b15155b919050565b90565b6060620003a5846200038b565b620004025760405162461bcd60e51b815260206004820152602660248201527f416464726573733a2064656c65676174652063616c6c20746f206e6f6e2d636f6044820152651b9d1c9858dd60d21b606482015260840162000240565b600080856001600160a01b0316856040516200041f9190620005b0565b600060405180830381855af49150503d80600081146200045c576040519150601f19603f3d011682016040523d82523d6000602084013e62000461565b606091505b509092509050620004748282866200047e565b9695505050505050565b606083156200048f575081620002bb565b825115620004a05782518084602001fd5b8160405162461bcd60e51b8152600401620002409190620005ce565b80516001600160a01b03811681146200039057600080fd5b600080600060608486031215620004e9578283fd5b620004f484620004bc565b92506200050460208501620004bc565b60408501519092506001600160401b038082111562000521578283fd5b818601915086601f83011262000535578283fd5b8151818111156200054a576200054a62000656565b604051601f8201601f19908116603f0116810190838211818310171562000575576200057562000656565b816040528281528960208487010111156200058e578586fd5b620005a183602083016020880162000627565b80955050505050509250925092565b60008251620005c481846020870162000627565b9190910192915050565b6000602082528251806020840152620005ef81604085016020870162000627565b601f01601f19169190910160400192915050565b6000828210156200062257634e487b7160e01b81526011600452602481fd5b500390565b60005b83811015620006445781810151838201526020016200062a565b83811115620001695750506000910152565b634e487b7160e01b600052604160045260246000fd5b61085d806200067c6000396000f3fe60806040526004361061004e5760003560e01c80633659cfe6146100655780634f1ef286146100855780635c60da1b146100985780638f283970146100c9578063f851a440146100e95761005d565b3661005d5761005b6100fe565b005b61005b6100fe565b34801561007157600080fd5b5061005b6100803660046106ed565b610118565b61005b610093366004610707565b610164565b3480156100a457600080fd5b506100ad6101da565b6040516001600160a01b03909116815260200160405180910390f35b3480156100d557600080fd5b5061005b6100e43660046106ed565b610217565b3480156100f557600080fd5b506100ad610241565b6101066102a2565b610116610111610346565b610355565b565b610120610379565b6001600160a01b0316336001600160a01b0316141561015957610154816040518060200160405280600081525060006103ac565b610161565b6101616100fe565b50565b61016c610379565b6001600160a01b0316336001600160a01b031614156101cd576101c88383838080601f016020809104026020016040519081016040528093929190818152602001838380828437600092019190915250600192506103ac915050565b6101d5565b6101d56100fe565b505050565b60006101e4610379565b6001600160a01b0316336001600160a01b0316141561020c57610205610346565b9050610214565b6102146100fe565b90565b61021f610379565b6001600160a01b0316336001600160a01b03161415610159576101548161040b565b600061024b610379565b6001600160a01b0316336001600160a01b0316141561020c57610205610379565b606061029183836040518060600160405280602781526020016108016027913961045f565b9392505050565b803b15155b919050565b6102aa610379565b6001600160a01b0316336001600160a01b031614156103415760405162461bcd60e51b815260206004820152604260248201527f5472616e73706172656e745570677261646561626c6550726f78793a2061646d60448201527f696e2063616e6e6f742066616c6c6261636b20746f2070726f78792074617267606482015261195d60f21b608482015260a4015b60405180910390fd5b610116565b600061035061053a565b905090565b3660008037600080366000845af43d6000803e808015610374573d6000f35b3d6000fd5b60007fb53127684a568b3173ae13b9f8a6016e243e63b6e8ee1178d6a717850b5d61035b546001600160a01b0316905090565b6103b583610562565b6040516001600160a01b038416907fbc7cd75a20ee27fd9adebab32041f755214dbc6bffa90cc0225b39da2e5c2d3b90600090a26000825111806103f65750805b156101d557610405838361026c565b50505050565b7f7e644d79422f17c01e4894b5f4f588d331ebfa28653d42ae832dc59e38c9798f610434610379565b604080516001600160a01b03928316815291841660208301520160405180910390a161016181610611565b606061046a84610298565b6104c55760405162461bcd60e51b815260206004820152602660248201527f416464726573733a2064656c65676174652063616c6c20746f206e6f6e2d636f6044820152651b9d1c9858dd60d21b6064820152608401610338565b600080856001600160a01b0316856040516104e09190610785565b600060405180830381855af49150503d806000811461051b576040519150601f19603f3d011682016040523d82523d6000602084013e610520565b606091505b509150915061053082828661069d565b9695505050505050565b60007f360894a13ba1a3210667c828492db98dca3e2076cc3735a920a3ca505d382bbc61039d565b61056b81610298565b6105cd5760405162461bcd60e51b815260206004820152602d60248201527f455243313936373a206e657720696d706c656d656e746174696f6e206973206e60448201526c1bdd08184818dbdb9d1c9858dd609a1b6064820152608401610338565b807f360894a13ba1a3210667c828492db98dca3e2076cc3735a920a3ca505d382bbc5b80546001600160a01b0319166001600160a01b039290921691909117905550565b6001600160a01b0381166106765760405162461bcd60e51b815260206004820152602660248201527f455243313936373a206e65772061646d696e20697320746865207a65726f206160448201526564647265737360d01b6064820152608401610338565b807fb53127684a568b3173ae13b9f8a6016e243e63b6e8ee1178d6a717850b5d61036105f0565b606083156106ac575081610291565b8251156106bc5782518084602001fd5b8160405162461bcd60e51b815260040161033891906107a1565b80356001600160a01b038116811461029d57600080fd5b6000602082840312156106fe578081fd5b610291826106d6565b60008060006040848603121561071b578182fd5b610724846106d6565b9250602084013567ffffffffffffffff80821115610740578384fd5b818601915086601f830112610753578384fd5b813581811115610761578485fd5b876020828501011115610772578485fd5b6020830194508093505050509250925092565b600082516107978184602087016107d4565b9190910192915050565b60006020825282518060208401526107c08160408501602087016107d4565b601f01601f19169190910160400192915050565b60005b838110156107ef5781810151838201526020016107d7565b83811115610405575050600091015256fe416464726573733a206c6f772d6c6576656c2064656c65676174652063616c6c206661696c6564a264697066735822122093f028255035b61df476b13b9dba3c4f06f60e51b9b4caee31680b389aef327f64736f6c63430008020033b53127684a568b3173ae13b9f8a6016e243e63b6e8ee1178d6a717850b5d6103360894a13ba1a3210667c828492db98dca3e2076cc3735a920a3ca505d382bbc416464726573733a206c6f772d6c6576656c2064656c65676174652063616c6c206661696c65640000000000000000000000002494d9e298d6113b237ec35d56898e0b7f3c62000000000000000000000000005daebaf064e893e6e4043e0605d75d08a861fdd9000000000000000000000000000000000000000000000000000000000000006000000000000000000000000000000000000000000000000000000000000000a4f7c00e63000000000000000000000000eb9b61b145d6489be575d3603f4a704810e143df00000000000000000000000004b9da42306b023f3572e106b11d82aad9d32ebb00000000000000000000000000000000000000000000000000000000000001770000000000000000000000008101cffbec8e045c3fadc3877a1d30f97d301209000000000000000000000000000000000000000000000000000009184e72a00000000000000000000000000000000000000000000000000000000000";
		public HeroSaleDeploymentBase() : base(BYTECODE) { }
		public HeroSaleDeploymentBase(string byteCode) : base(byteCode) { }

	}

	public partial class BidderRoleFunction : BidderRoleFunctionBase { }

	[Function("BIDDER_ROLE", "bytes32")]
	public class BidderRoleFunctionBase : FunctionMessage
	{

	}

	public partial class DefaultAdminRoleFunction : DefaultAdminRoleFunctionBase { }

	[Function("DEFAULT_ADMIN_ROLE", "bytes32")]
	public class DefaultAdminRoleFunctionBase : FunctionMessage
	{

	}

	public partial class Erc721Function : Erc721FunctionBase { }

	[Function("ERC721", "address")]
	public class Erc721FunctionBase : FunctionMessage
	{

	}

	public partial class ModeratorRoleFunction : ModeratorRoleFunctionBase { }

	[Function("MODERATOR_ROLE", "bytes32")]
	public class ModeratorRoleFunctionBase : FunctionMessage
	{

	}

	public partial class AssistingAuctionFunction : AssistingAuctionFunctionBase { }

	[Function("assistingAuction", "address")]
	public class AssistingAuctionFunctionBase : FunctionMessage
	{

	}

	public partial class AuctionIdOffsetFunction : AuctionIdOffsetFunctionBase { }

	[Function("auctionIdOffset", "uint256")]
	public class AuctionIdOffsetFunctionBase : FunctionMessage
	{

	}

	public partial class AuctionsFunction : AuctionsFunctionBase { }

	[Function("auctions", typeof(AuctionsOutputDTO))]
	public class AuctionsFunctionBase : FunctionMessage
	{
		[Parameter("uint256", "", 1)]
		public virtual BigInteger ReturnValue1 { get; set; }
	}

	public partial class BidFunction : BidFunctionBase { }

	[Function("bid")]
	public class BidFunctionBase : FunctionMessage
	{
		[Parameter("uint256", "_tokenId", 1)]
		public virtual BigInteger TokenId { get; set; }
		[Parameter("uint256", "_bidAmount", 2)]
		public virtual BigInteger BidAmount { get; set; }
	}

	public partial class BidForFunction : BidForFunctionBase { }

	[Function("bidFor")]
	public class BidForFunctionBase : FunctionMessage
	{
		[Parameter("address", "_bidder", 1)]
		public virtual string Bidder { get; set; }
		[Parameter("uint256", "_tokenId", 2)]
		public virtual BigInteger TokenId { get; set; }
		[Parameter("uint256", "_bidAmount", 3)]
		public virtual BigInteger BidAmount { get; set; }
	}

	public partial class CancelAuctionFunction : CancelAuctionFunctionBase { }

	[Function("cancelAuction")]
	public class CancelAuctionFunctionBase : FunctionMessage
	{
		[Parameter("uint256", "_tokenId", 1)]
		public virtual BigInteger TokenId { get; set; }
	}

	public partial class CancelAuctionWhenPausedFunction : CancelAuctionWhenPausedFunctionBase { }

	[Function("cancelAuctionWhenPaused")]
	public class CancelAuctionWhenPausedFunctionBase : FunctionMessage
	{
		[Parameter("uint256", "_tokenId", 1)]
		public virtual BigInteger TokenId { get; set; }
	}

	public partial class CreateAuctionFunction : CreateAuctionFunctionBase { }

	[Function("createAuction")]
	public class CreateAuctionFunctionBase : FunctionMessage
	{
		[Parameter("uint256", "_tokenId", 1)]
		public virtual BigInteger TokenId { get; set; }
		[Parameter("uint128", "_startingPrice", 2)]
		public virtual BigInteger StartingPrice { get; set; }
		[Parameter("uint128", "_endingPrice", 3)]
		public virtual BigInteger EndingPrice { get; set; }
		[Parameter("uint64", "_duration", 4)]
		public virtual ulong Duration { get; set; }
		[Parameter("address", "_winner", 5)]
		public virtual string Winner { get; set; }
	}

	public partial class FeeAddressesFunction : FeeAddressesFunctionBase { }

	[Function("feeAddresses", "address")]
	public class FeeAddressesFunctionBase : FunctionMessage
	{
		[Parameter("uint256", "", 1)]
		public virtual BigInteger ReturnValue1 { get; set; }
	}

	public partial class FeePercentsFunction : FeePercentsFunctionBase { }

	[Function("feePercents", "uint256")]
	public class FeePercentsFunctionBase : FunctionMessage
	{
		[Parameter("uint256", "", 1)]
		public virtual BigInteger ReturnValue1 { get; set; }
	}

	public partial class GetAuctionFunction : GetAuctionFunctionBase { }

	[Function("getAuction", typeof(GetAuctionOutputDTO))]
	public class GetAuctionFunctionBase : FunctionMessage
	{
		[Parameter("uint256", "_tokenId", 1)]
		public virtual BigInteger TokenId { get; set; }
	}

	public partial class GetAuctionsFunction : GetAuctionsFunctionBase { }

	[Function("getAuctions", typeof(GetAuctionsOutputDTO))]
	public class GetAuctionsFunctionBase : FunctionMessage
	{
		[Parameter("uint256[]", "_tokenIds", 1)]
		public virtual List<BigInteger> TokenIds { get; set; }
	}

	public partial class GetCurrentPriceFunction : GetCurrentPriceFunctionBase { }

	[Function("getCurrentPrice", "uint256")]
	public class GetCurrentPriceFunctionBase : FunctionMessage
	{
		[Parameter("uint256", "_tokenId", 1)]
		public virtual BigInteger TokenId { get; set; }
	}

	public partial class GetRoleAdminFunction : GetRoleAdminFunctionBase { }

	[Function("getRoleAdmin", "bytes32")]
	public class GetRoleAdminFunctionBase : FunctionMessage
	{
		[Parameter("bytes32", "role", 1)]
		public virtual byte[] Role { get; set; }
	}

	public partial class GetUserAuctionsFunction : GetUserAuctionsFunctionBase { }

	[Function("getUserAuctions", "uint256[]")]
	public class GetUserAuctionsFunctionBase : FunctionMessage
	{
		[Parameter("address", "_address", 1)]
		public virtual string Address { get; set; }
	}

	public partial class GrantRoleFunction : GrantRoleFunctionBase { }

	[Function("grantRole")]
	public class GrantRoleFunctionBase : FunctionMessage
	{
		[Parameter("bytes32", "role", 1)]
		public virtual byte[] Role { get; set; }
		[Parameter("address", "account", 2)]
		public virtual string Account { get; set; }
	}

	public partial class HasRoleFunction : HasRoleFunctionBase { }

	[Function("hasRole", "bool")]
	public class HasRoleFunctionBase : FunctionMessage
	{
		[Parameter("bytes32", "role", 1)]
		public virtual byte[] Role { get; set; }
		[Parameter("address", "account", 2)]
		public virtual string Account { get; set; }
	}

	public partial class InitializeFunction : InitializeFunctionBase { }

	[Function("initialize")]
	public class InitializeFunctionBase : FunctionMessage
	{
		[Parameter("address", "_heroCoreAddress", 1)]
		public virtual string HeroCoreAddress { get; set; }
		[Parameter("address", "_crystalAddress", 2)]
		public virtual string CrystalAddress { get; set; }
		[Parameter("uint256", "_cut", 3)]
		public virtual BigInteger Cut { get; set; }
		[Parameter("address", "_assistingAuctionAddress", 4)]
		public virtual string AssistingAuctionAddress { get; set; }
		[Parameter("uint256", "_auctionIdOffset", 5)]
		public virtual BigInteger AuctionIdOffset { get; set; }
	}

	public partial class IsOnAuctionFunction : IsOnAuctionFunctionBase { }

	[Function("isOnAuction", "bool")]
	public class IsOnAuctionFunctionBase : FunctionMessage
	{
		[Parameter("uint256", "_tokenId", 1)]
		public virtual BigInteger TokenId { get; set; }
	}

	public partial class OnERC721ReceivedFunction : OnERC721ReceivedFunctionBase { }

	[Function("onERC721Received", "bytes4")]
	public class OnERC721ReceivedFunctionBase : FunctionMessage
	{
		[Parameter("address", "", 1)]
		public virtual string ReturnValue1 { get; set; }
		[Parameter("address", "", 2)]
		public virtual string ReturnValue2 { get; set; }
		[Parameter("uint256", "", 3)]
		public virtual BigInteger ReturnValue3 { get; set; }
		[Parameter("bytes", "", 4)]
		public virtual byte[] ReturnValue4 { get; set; }
	}

	public partial class OwnerCutFunction : OwnerCutFunctionBase { }

	[Function("ownerCut", "uint256")]
	public class OwnerCutFunctionBase : FunctionMessage
	{

	}

	public partial class PauseFunction : PauseFunctionBase { }

	[Function("pause")]
	public class PauseFunctionBase : FunctionMessage
	{

	}

	public partial class PausedFunction : PausedFunctionBase { }

	[Function("paused", "bool")]
	public class PausedFunctionBase : FunctionMessage
	{

	}

	public partial class PowerTokenFunction : PowerTokenFunctionBase { }

	[Function("powerToken", "address")]
	public class PowerTokenFunctionBase : FunctionMessage
	{

	}

	public partial class RenounceRoleFunction : RenounceRoleFunctionBase { }

	[Function("renounceRole")]
	public class RenounceRoleFunctionBase : FunctionMessage
	{
		[Parameter("bytes32", "role", 1)]
		public virtual byte[] Role { get; set; }
		[Parameter("address", "account", 2)]
		public virtual string Account { get; set; }
	}

	public partial class RevokeRoleFunction : RevokeRoleFunctionBase { }

	[Function("revokeRole")]
	public class RevokeRoleFunctionBase : FunctionMessage
	{
		[Parameter("bytes32", "role", 1)]
		public virtual byte[] Role { get; set; }
		[Parameter("address", "account", 2)]
		public virtual string Account { get; set; }
	}

	public partial class SetAuctionIDOffsetFunction : SetAuctionIDOffsetFunctionBase { }

	[Function("setAuctionIDOffset")]
	public class SetAuctionIDOffsetFunctionBase : FunctionMessage
	{
		[Parameter("uint256", "_auctionIdOffset", 1)]
		public virtual BigInteger AuctionIdOffset { get; set; }
	}

	public partial class SetERC721Function : SetERC721FunctionBase { }

	[Function("setERC721")]
	public class SetERC721FunctionBase : FunctionMessage
	{
		[Parameter("address", "_newERC721Address", 1)]
		public virtual string NewERC721Address { get; set; }
	}

	public partial class SetFeesFunction : SetFeesFunctionBase { }

	[Function("setFees")]
	public class SetFeesFunctionBase : FunctionMessage
	{
		[Parameter("address[]", "_feeAddresses", 1)]
		public virtual List<string> FeeAddresses { get; set; }
		[Parameter("uint256[]", "_feePercents", 2)]
		public virtual List<BigInteger> FeePercents { get; set; }
	}

	public partial class SupportsInterfaceFunction : SupportsInterfaceFunctionBase { }

	[Function("supportsInterface", "bool")]
	public class SupportsInterfaceFunctionBase : FunctionMessage
	{
		[Parameter("bytes4", "interfaceId", 1)]
		public virtual byte[] InterfaceId { get; set; }
	}

	public partial class TotalAuctionsFunction : TotalAuctionsFunctionBase { }

	[Function("totalAuctions", "uint256")]
	public class TotalAuctionsFunctionBase : FunctionMessage
	{

	}

	public partial class UnpauseFunction : UnpauseFunctionBase { }

	[Function("unpause")]
	public class UnpauseFunctionBase : FunctionMessage
	{

	}

	public partial class UserAuctionsFunction : UserAuctionsFunctionBase { }

	[Function("userAuctions", "uint256")]
	public class UserAuctionsFunctionBase : FunctionMessage
	{
		[Parameter("address", "", 1)]
		public virtual string ReturnValue1 { get; set; }
		[Parameter("uint256", "", 2)]
		public virtual BigInteger ReturnValue2 { get; set; }
	}

	public partial class AuctionCancelledEventDTO : AuctionCancelledEventDTOBase { }

	[Event("AuctionCancelled")]
	public class AuctionCancelledEventDTOBase : IEventDTO
	{
		[Parameter("uint256", "auctionId", 1, false)]
		public virtual BigInteger AuctionId { get; set; }
		[Parameter("uint256", "tokenId", 2, true)]
		public virtual BigInteger TokenId { get; set; }
	}

	public partial class AuctionCreatedEventDTO : AuctionCreatedEventDTOBase { }

	[Event("AuctionCreated")]
	public class AuctionCreatedEventDTOBase : IEventDTO
	{
		[Parameter("uint256", "auctionId", 1, false)]
		public virtual BigInteger AuctionId { get; set; }
		[Parameter("address", "owner", 2, true)]
		public virtual string Owner { get; set; }
		[Parameter("uint256", "tokenId", 3, true)]
		public virtual BigInteger TokenId { get; set; }
		[Parameter("uint256", "startingPrice", 4, false)]
		public virtual BigInteger StartingPrice { get; set; }
		[Parameter("uint256", "endingPrice", 5, false)]
		public virtual BigInteger EndingPrice { get; set; }
		[Parameter("uint256", "duration", 6, false)]
		public virtual BigInteger Duration { get; set; }
		[Parameter("address", "winner", 7, false)]
		public virtual string Winner { get; set; }
	}

	public partial class AuctionSuccessfulEventDTO : AuctionSuccessfulEventDTOBase { }

	[Event("AuctionSuccessful")]
	public class AuctionSuccessfulEventDTOBase : IEventDTO
	{
		[Parameter("uint256", "auctionId", 1, false)]
		public virtual BigInteger AuctionId { get; set; }
		[Parameter("uint256", "tokenId", 2, true)]
		public virtual BigInteger TokenId { get; set; }
		[Parameter("uint256", "totalPrice", 3, false)]
		public virtual BigInteger TotalPrice { get; set; }
		[Parameter("address", "winner", 4, false)]
		public virtual string Winner { get; set; }
	}

	public partial class InitializedEventDTO : InitializedEventDTOBase { }

	[Event("Initialized")]
	public class InitializedEventDTOBase : IEventDTO
	{
		[Parameter("uint8", "version", 1, false)]
		public virtual byte Version { get; set; }
	}

	public partial class PausedEventDTO : PausedEventDTOBase { }

	[Event("Paused")]
	public class PausedEventDTOBase : IEventDTO
	{
		[Parameter("address", "account", 1, false)]
		public virtual string Account { get; set; }
	}

	public partial class RoleAdminChangedEventDTO : RoleAdminChangedEventDTOBase { }

	[Event("RoleAdminChanged")]
	public class RoleAdminChangedEventDTOBase : IEventDTO
	{
		[Parameter("bytes32", "role", 1, true)]
		public virtual byte[] Role { get; set; }
		[Parameter("bytes32", "previousAdminRole", 2, true)]
		public virtual byte[] PreviousAdminRole { get; set; }
		[Parameter("bytes32", "newAdminRole", 3, true)]
		public virtual byte[] NewAdminRole { get; set; }
	}

	public partial class RoleGrantedEventDTO : RoleGrantedEventDTOBase { }

	[Event("RoleGranted")]
	public class RoleGrantedEventDTOBase : IEventDTO
	{
		[Parameter("bytes32", "role", 1, true)]
		public virtual byte[] Role { get; set; }
		[Parameter("address", "account", 2, true)]
		public virtual string Account { get; set; }
		[Parameter("address", "sender", 3, true)]
		public virtual string Sender { get; set; }
	}

	public partial class RoleRevokedEventDTO : RoleRevokedEventDTOBase { }

	[Event("RoleRevoked")]
	public class RoleRevokedEventDTOBase : IEventDTO
	{
		[Parameter("bytes32", "role", 1, true)]
		public virtual byte[] Role { get; set; }
		[Parameter("address", "account", 2, true)]
		public virtual string Account { get; set; }
		[Parameter("address", "sender", 3, true)]
		public virtual string Sender { get; set; }
	}

	public partial class UnpausedEventDTO : UnpausedEventDTOBase { }

	[Event("Unpaused")]
	public class UnpausedEventDTOBase : IEventDTO
	{
		[Parameter("address", "account", 1, false)]
		public virtual string Account { get; set; }
	}

	public partial class BidderRoleOutputDTO : BidderRoleOutputDTOBase { }

	[FunctionOutput]
	public class BidderRoleOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("bytes32", "", 1)]
		public virtual byte[] ReturnValue1 { get; set; }
	}

	public partial class DefaultAdminRoleOutputDTO : DefaultAdminRoleOutputDTOBase { }

	[FunctionOutput]
	public class DefaultAdminRoleOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("bytes32", "", 1)]
		public virtual byte[] ReturnValue1 { get; set; }
	}

	public partial class Erc721OutputDTO : Erc721OutputDTOBase { }

	[FunctionOutput]
	public class Erc721OutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("address", "", 1)]
		public virtual string ReturnValue1 { get; set; }
	}

	public partial class ModeratorRoleOutputDTO : ModeratorRoleOutputDTOBase { }

	[FunctionOutput]
	public class ModeratorRoleOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("bytes32", "", 1)]
		public virtual byte[] ReturnValue1 { get; set; }
	}

	public partial class AssistingAuctionOutputDTO : AssistingAuctionOutputDTOBase { }

	[FunctionOutput]
	public class AssistingAuctionOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("address", "", 1)]
		public virtual string ReturnValue1 { get; set; }
	}

	public partial class AuctionIdOffsetOutputDTO : AuctionIdOffsetOutputDTOBase { }

	[FunctionOutput]
	public class AuctionIdOffsetOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("uint256", "", 1)]
		public virtual BigInteger ReturnValue1 { get; set; }
	}

	public partial class AuctionsOutputDTO : AuctionsOutputDTOBase { }

	[FunctionOutput]
	public class AuctionsOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("address", "seller", 1)]
		public virtual string Seller { get; set; }
		[Parameter("uint256", "tokenId", 2)]
		public virtual BigInteger TokenId { get; set; }
		[Parameter("uint128", "startingPrice", 3)]
		public virtual BigInteger StartingPrice { get; set; }
		[Parameter("uint128", "endingPrice", 4)]
		public virtual BigInteger EndingPrice { get; set; }
		[Parameter("uint64", "duration", 5)]
		public virtual ulong Duration { get; set; }
		[Parameter("uint64", "startedAt", 6)]
		public virtual ulong StartedAt { get; set; }
		[Parameter("address", "winner", 7)]
		public virtual string Winner { get; set; }
		[Parameter("bool", "open", 8)]
		public virtual bool Open { get; set; }
	}











	public partial class FeeAddressesOutputDTO : FeeAddressesOutputDTOBase { }

	[FunctionOutput]
	public class FeeAddressesOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("address", "", 1)]
		public virtual string ReturnValue1 { get; set; }
	}

	public partial class FeePercentsOutputDTO : FeePercentsOutputDTOBase { }

	[FunctionOutput]
	public class FeePercentsOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("uint256", "", 1)]
		public virtual BigInteger ReturnValue1 { get; set; }
	}

	public partial class GetAuctionOutputDTO : GetAuctionOutputDTOBase { }

	[FunctionOutput]
	public class GetAuctionOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("tuple", "", 1)]
		public virtual Auction ReturnValue1 { get; set; }
	}

	public partial class GetAuctionsOutputDTO : GetAuctionsOutputDTOBase { }

	[FunctionOutput]
	public class GetAuctionsOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("tuple[]", "", 1)]
		public virtual List<Auction> ReturnValue1 { get; set; }
	}

	public partial class GetCurrentPriceOutputDTO : GetCurrentPriceOutputDTOBase { }

	[FunctionOutput]
	public class GetCurrentPriceOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("uint256", "", 1)]
		public virtual BigInteger ReturnValue1 { get; set; }
	}

	public partial class GetRoleAdminOutputDTO : GetRoleAdminOutputDTOBase { }

	[FunctionOutput]
	public class GetRoleAdminOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("bytes32", "", 1)]
		public virtual byte[] ReturnValue1 { get; set; }
	}

	public partial class GetUserAuctionsOutputDTO : GetUserAuctionsOutputDTOBase { }

	[FunctionOutput]
	public class GetUserAuctionsOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("uint256[]", "", 1)]
		public virtual List<BigInteger> ReturnValue1 { get; set; }
	}



	public partial class HasRoleOutputDTO : HasRoleOutputDTOBase { }

	[FunctionOutput]
	public class HasRoleOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("bool", "", 1)]
		public virtual bool ReturnValue1 { get; set; }
	}



	public partial class IsOnAuctionOutputDTO : IsOnAuctionOutputDTOBase { }

	[FunctionOutput]
	public class IsOnAuctionOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("bool", "", 1)]
		public virtual bool ReturnValue1 { get; set; }
	}

	public partial class OnERC721ReceivedOutputDTO : OnERC721ReceivedOutputDTOBase { }

	[FunctionOutput]
	public class OnERC721ReceivedOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("bytes4", "", 1)]
		public virtual byte[] ReturnValue1 { get; set; }
	}

	public partial class OwnerCutOutputDTO : OwnerCutOutputDTOBase { }

	[FunctionOutput]
	public class OwnerCutOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("uint256", "", 1)]
		public virtual BigInteger ReturnValue1 { get; set; }
	}



	public partial class PausedOutputDTO : PausedOutputDTOBase { }

	[FunctionOutput]
	public class PausedOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("bool", "", 1)]
		public virtual bool ReturnValue1 { get; set; }
	}

	public partial class PowerTokenOutputDTO : PowerTokenOutputDTOBase { }

	[FunctionOutput]
	public class PowerTokenOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("address", "", 1)]
		public virtual string ReturnValue1 { get; set; }
	}











	public partial class SupportsInterfaceOutputDTO : SupportsInterfaceOutputDTOBase { }

	[FunctionOutput]
	public class SupportsInterfaceOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("bool", "", 1)]
		public virtual bool ReturnValue1 { get; set; }
	}

	public partial class TotalAuctionsOutputDTO : TotalAuctionsOutputDTOBase { }

	[FunctionOutput]
	public class TotalAuctionsOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("uint256", "", 1)]
		public virtual BigInteger ReturnValue1 { get; set; }
	}



	public partial class UserAuctionsOutputDTO : UserAuctionsOutputDTOBase { }

	[FunctionOutput]
	public class UserAuctionsOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("uint256", "", 1)]
		public virtual BigInteger ReturnValue1 { get; set; }
	}
}
