using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System.Numerics;

namespace DFKContracts.HeroCore.ContractDefinition
{


	public partial class HeroCoreDeployment : HeroCoreDeploymentBase
	{
		public HeroCoreDeployment() : base(BYTECODE) { }
		public HeroCoreDeployment(string byteCode) : base(byteCode) { }
	}

	public class HeroCoreDeploymentBase : ContractDeploymentMessage
	{
		public static string BYTECODE = "608060405260405162000f4038038062000f408339810160408190526200002691620004d4565b82816200005560017f360894a13ba1a3210667c828492db98dca3e2076cc3735a920a3ca505d382bbd62000603565b60008051602062000ef9833981519152146200008157634e487b7160e01b600052600160045260246000fd5b6200008f82826000620000ff565b50620000bf905060017fb53127684a568b3173ae13b9f8a6016e243e63b6e8ee1178d6a717850b5d610462000603565b60008051602062000ed983398151915214620000eb57634e487b7160e01b600052600160045260246000fd5b620000f68262000170565b5050506200066c565b6200010a83620001cb565b6040516001600160a01b038416907fbc7cd75a20ee27fd9adebab32041f755214dbc6bffa90cc0225b39da2e5c2d3b90600090a26000825111806200014c5750805b156200016b576200016983836200029360201b6200026c1760201c565b505b505050565b7f7e644d79422f17c01e4894b5f4f588d331ebfa28653d42ae832dc59e38c9798f6200019b620002c2565b604080516001600160a01b03928316815291841660208301520160405180910390a1620001c881620002fb565b50565b620001e1816200038b60201b620002981760201c565b620002495760405162461bcd60e51b815260206004820152602d60248201527f455243313936373a206e657720696d706c656d656e746174696f6e206973206e60448201526c1bdd08184818dbdb9d1c9858dd609a1b60648201526084015b60405180910390fd5b806200027260008051602062000ef983398151915260001b6200039560201b620002141760201c565b80546001600160a01b0319166001600160a01b039290921691909117905550565b6060620002bb838360405180606001604052806027815260200162000f196027913962000398565b9392505050565b6000620002ec60008051602062000ed983398151915260001b6200039560201b620002141760201c565b546001600160a01b0316905090565b6001600160a01b038116620003625760405162461bcd60e51b815260206004820152602660248201527f455243313936373a206e65772061646d696e20697320746865207a65726f206160448201526564647265737360d01b606482015260840162000240565b806200027260008051602062000ed983398151915260001b6200039560201b620002141760201c565b803b15155b919050565b90565b6060620003a5846200038b565b620004025760405162461bcd60e51b815260206004820152602660248201527f416464726573733a2064656c65676174652063616c6c20746f206e6f6e2d636f6044820152651b9d1c9858dd60d21b606482015260840162000240565b600080856001600160a01b0316856040516200041f9190620005b0565b600060405180830381855af49150503d80600081146200045c576040519150601f19603f3d011682016040523d82523d6000602084013e62000461565b606091505b509092509050620004748282866200047e565b9695505050505050565b606083156200048f575081620002bb565b825115620004a05782518084602001fd5b8160405162461bcd60e51b8152600401620002409190620005ce565b80516001600160a01b03811681146200039057600080fd5b600080600060608486031215620004e9578283fd5b620004f484620004bc565b92506200050460208501620004bc565b60408501519092506001600160401b038082111562000521578283fd5b818601915086601f83011262000535578283fd5b8151818111156200054a576200054a62000656565b604051601f8201601f19908116603f0116810190838211818310171562000575576200057562000656565b816040528281528960208487010111156200058e578586fd5b620005a183602083016020880162000627565b80955050505050509250925092565b60008251620005c481846020870162000627565b9190910192915050565b6000602082528251806020840152620005ef81604085016020870162000627565b601f01601f19169190910160400192915050565b6000828210156200062257634e487b7160e01b81526011600452602481fd5b500390565b60005b83811015620006445781810151838201526020016200062a565b83811115620001695750506000910152565b634e487b7160e01b600052604160045260246000fd5b61085d806200067c6000396000f3fe60806040526004361061004e5760003560e01c80633659cfe6146100655780634f1ef286146100855780635c60da1b146100985780638f283970146100c9578063f851a440146100e95761005d565b3661005d5761005b6100fe565b005b61005b6100fe565b34801561007157600080fd5b5061005b6100803660046106ed565b610118565b61005b610093366004610707565b610164565b3480156100a457600080fd5b506100ad6101da565b6040516001600160a01b03909116815260200160405180910390f35b3480156100d557600080fd5b5061005b6100e43660046106ed565b610217565b3480156100f557600080fd5b506100ad610241565b6101066102a2565b610116610111610346565b610355565b565b610120610379565b6001600160a01b0316336001600160a01b0316141561015957610154816040518060200160405280600081525060006103ac565b610161565b6101616100fe565b50565b61016c610379565b6001600160a01b0316336001600160a01b031614156101cd576101c88383838080601f016020809104026020016040519081016040528093929190818152602001838380828437600092019190915250600192506103ac915050565b6101d5565b6101d56100fe565b505050565b60006101e4610379565b6001600160a01b0316336001600160a01b0316141561020c57610205610346565b9050610214565b6102146100fe565b90565b61021f610379565b6001600160a01b0316336001600160a01b03161415610159576101548161040b565b600061024b610379565b6001600160a01b0316336001600160a01b0316141561020c57610205610379565b606061029183836040518060600160405280602781526020016108016027913961045f565b9392505050565b803b15155b919050565b6102aa610379565b6001600160a01b0316336001600160a01b031614156103415760405162461bcd60e51b815260206004820152604260248201527f5472616e73706172656e745570677261646561626c6550726f78793a2061646d60448201527f696e2063616e6e6f742066616c6c6261636b20746f2070726f78792074617267606482015261195d60f21b608482015260a4015b60405180910390fd5b610116565b600061035061053a565b905090565b3660008037600080366000845af43d6000803e808015610374573d6000f35b3d6000fd5b60007fb53127684a568b3173ae13b9f8a6016e243e63b6e8ee1178d6a717850b5d61035b546001600160a01b0316905090565b6103b583610562565b6040516001600160a01b038416907fbc7cd75a20ee27fd9adebab32041f755214dbc6bffa90cc0225b39da2e5c2d3b90600090a26000825111806103f65750805b156101d557610405838361026c565b50505050565b7f7e644d79422f17c01e4894b5f4f588d331ebfa28653d42ae832dc59e38c9798f610434610379565b604080516001600160a01b03928316815291841660208301520160405180910390a161016181610611565b606061046a84610298565b6104c55760405162461bcd60e51b815260206004820152602660248201527f416464726573733a2064656c65676174652063616c6c20746f206e6f6e2d636f6044820152651b9d1c9858dd60d21b6064820152608401610338565b600080856001600160a01b0316856040516104e09190610785565b600060405180830381855af49150503d806000811461051b576040519150601f19603f3d011682016040523d82523d6000602084013e610520565b606091505b509150915061053082828661069d565b9695505050505050565b60007f360894a13ba1a3210667c828492db98dca3e2076cc3735a920a3ca505d382bbc61039d565b61056b81610298565b6105cd5760405162461bcd60e51b815260206004820152602d60248201527f455243313936373a206e657720696d706c656d656e746174696f6e206973206e60448201526c1bdd08184818dbdb9d1c9858dd609a1b6064820152608401610338565b807f360894a13ba1a3210667c828492db98dca3e2076cc3735a920a3ca505d382bbc5b80546001600160a01b0319166001600160a01b039290921691909117905550565b6001600160a01b0381166106765760405162461bcd60e51b815260206004820152602660248201527f455243313936373a206e65772061646d696e20697320746865207a65726f206160448201526564647265737360d01b6064820152608401610338565b807fb53127684a568b3173ae13b9f8a6016e243e63b6e8ee1178d6a717850b5d61036105f0565b606083156106ac575081610291565b8251156106bc5782518084602001fd5b8160405162461bcd60e51b815260040161033891906107a1565b80356001600160a01b038116811461029d57600080fd5b6000602082840312156106fe578081fd5b610291826106d6565b60008060006040848603121561071b578182fd5b610724846106d6565b9250602084013567ffffffffffffffff80821115610740578384fd5b818601915086601f830112610753578384fd5b813581811115610761578485fd5b876020828501011115610772578485fd5b6020830194508093505050509250925092565b600082516107978184602087016107d4565b9190910192915050565b60006020825282518060208401526107c08160408501602087016107d4565b601f01601f19169190910160400192915050565b60005b838110156107ef5781810151838201526020016107d7565b83811115610405575050600091015256fe416464726573733a206c6f772d6c6576656c2064656c65676174652063616c6c206661696c6564a264697066735822122093f028255035b61df476b13b9dba3c4f06f60e51b9b4caee31680b389aef327f64736f6c63430008020033b53127684a568b3173ae13b9f8a6016e243e63b6e8ee1178d6a717850b5d6103360894a13ba1a3210667c828492db98dca3e2076cc3735a920a3ca505d382bbc416464726573733a206c6f772d6c6576656c2064656c65676174652063616c6c206661696c6564000000000000000000000000725b350797e675293947c25e16a7d718bd0e45950000000000000000000000005daebaf064e893e6e4043e0605d75d08a861fdd9000000000000000000000000000000000000000000000000000000000000006000000000000000000000000000000000000000000000000000000000000000e4077f224a000000000000000000000000000000000000000000000000000000000000006000000000000000000000000000000000000000000000000000000000000000a00000000000000000000000003399313f7f0b179d874e16f9e0a5b3962e8222f7000000000000000000000000000000000000000000000000000000000000000844464b204865726f000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000744464b4845524f0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
		public HeroCoreDeploymentBase() : base(BYTECODE) { }
		public HeroCoreDeploymentBase(string byteCode) : base(byteCode) { }

	}

	public partial class BridgeRoleFunction : BridgeRoleFunctionBase { }

	[Function("BRIDGE_ROLE", "bytes32")]
	public class BridgeRoleFunctionBase : FunctionMessage
	{

	}

	public partial class DefaultAdminRoleFunction : DefaultAdminRoleFunctionBase { }

	[Function("DEFAULT_ADMIN_ROLE", "bytes32")]
	public class DefaultAdminRoleFunctionBase : FunctionMessage
	{

	}

	public partial class HeroModeratorRoleFunction : HeroModeratorRoleFunctionBase { }

	[Function("HERO_MODERATOR_ROLE", "bytes32")]
	public class HeroModeratorRoleFunctionBase : FunctionMessage
	{

	}

	public partial class MinterRoleFunction : MinterRoleFunctionBase { }

	[Function("MINTER_ROLE", "bytes32")]
	public class MinterRoleFunctionBase : FunctionMessage
	{

	}

	public partial class ModeratorRoleFunction : ModeratorRoleFunctionBase { }

	[Function("MODERATOR_ROLE", "bytes32")]
	public class ModeratorRoleFunctionBase : FunctionMessage
	{

	}

	public partial class ApproveFunction : ApproveFunctionBase { }

	[Function("approve")]
	public class ApproveFunctionBase : FunctionMessage
	{
		[Parameter("address", "to", 1)]
		public virtual string To { get; set; }
		[Parameter("uint256", "tokenId", 2)]
		public virtual BigInteger TokenId { get; set; }
	}

	public partial class BalanceOfFunction : BalanceOfFunctionBase { }

	[Function("balanceOf", "uint256")]
	public class BalanceOfFunctionBase : FunctionMessage
	{
		[Parameter("address", "owner", 1)]
		public virtual string Owner { get; set; }
	}

	public partial class BridgeMintFunction : BridgeMintFunctionBase { }

	[Function("bridgeMint")]
	public class BridgeMintFunctionBase : FunctionMessage
	{
		[Parameter("uint256", "_id", 1)]
		public virtual BigInteger Id { get; set; }
		[Parameter("address", "_to", 2)]
		public virtual string To { get; set; }
	}

	public partial class CreateHeroFunction : CreateHeroFunctionBase { }

	[Function("createHero", "uint256")]
	public class CreateHeroFunctionBase : FunctionMessage
	{
		[Parameter("uint256", "_statGenes", 1)]
		public virtual BigInteger StatGenes { get; set; }
		[Parameter("uint256", "_visualGenes", 2)]
		public virtual BigInteger VisualGenes { get; set; }
		[Parameter("uint8", "_rarity", 3)]
		public virtual byte Rarity { get; set; }
		[Parameter("bool", "_shiny", 4)]
		public virtual bool Shiny { get; set; }
		[Parameter("tuple", "_crystal", 5)]
		public virtual HeroCrystal Crystal { get; set; }
		[Parameter("uint256", "_crystalId", 6)]
		public virtual BigInteger CrystalId { get; set; }
	}

	public partial class GetApprovedFunction : GetApprovedFunctionBase { }

	[Function("getApproved", "address")]
	public class GetApprovedFunctionBase : FunctionMessage
	{
		[Parameter("uint256", "tokenId", 1)]
		public virtual BigInteger TokenId { get; set; }
	}

	public partial class GetHeroFunction : GetHeroFunctionBase { }

	[Function("getHero", typeof(GetHeroOutputDTO))]
	public class GetHeroFunctionBase : FunctionMessage
	{
		[Parameter("uint256", "_id", 1)]
		public virtual BigInteger Id { get; set; }
	}

	public partial class GetRoleAdminFunction : GetRoleAdminFunctionBase { }

	[Function("getRoleAdmin", "bytes32")]
	public class GetRoleAdminFunctionBase : FunctionMessage
	{
		[Parameter("bytes32", "role", 1)]
		public virtual byte[] Role { get; set; }
	}

	public partial class GetUserHeroesFunction : GetUserHeroesFunctionBase { }

	[Function("getUserHeroes", "uint256[]")]
	public class GetUserHeroesFunctionBase : FunctionMessage
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

	public partial class HeroesFunction : HeroesFunctionBase { }

	[Function("heroes", typeof(HeroesOutputDTO))]
	public class HeroesFunctionBase : FunctionMessage
	{
		[Parameter("uint256", "", 1)]
		public virtual BigInteger ReturnValue1 { get; set; }
	}

	public partial class InitializeFunction : InitializeFunctionBase { }

	[Function("initialize")]
	public class InitializeFunctionBase : FunctionMessage
	{
		[Parameter("string", "_name", 1)]
		public virtual string Name { get; set; }
		[Parameter("string", "_symbol", 2)]
		public virtual string Symbol { get; set; }
		[Parameter("address", "_statScience", 3)]
		public virtual string StatScience { get; set; }
	}

	public partial class IsApprovedForAllFunction : IsApprovedForAllFunctionBase { }

	[Function("isApprovedForAll", "bool")]
	public class IsApprovedForAllFunctionBase : FunctionMessage
	{
		[Parameter("address", "owner", 1)]
		public virtual string Owner { get; set; }
		[Parameter("address", "operator", 2)]
		public virtual string Operator { get; set; }
	}

	public partial class NameFunction : NameFunctionBase { }

	[Function("name", "string")]
	public class NameFunctionBase : FunctionMessage
	{

	}

	public partial class NextHeroIdFunction : NextHeroIdFunctionBase { }

	[Function("nextHeroId", "uint256")]
	public class NextHeroIdFunctionBase : FunctionMessage
	{

	}

	public partial class OwnerOfFunction : OwnerOfFunctionBase { }

	[Function("ownerOf", "address")]
	public class OwnerOfFunctionBase : FunctionMessage
	{
		[Parameter("uint256", "tokenId", 1)]
		public virtual BigInteger TokenId { get; set; }
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

	public partial class SafeTransferFromFunction : SafeTransferFromFunctionBase { }

	[Function("safeTransferFrom")]
	public class SafeTransferFromFunctionBase : FunctionMessage
	{
		[Parameter("address", "from", 1)]
		public virtual string From { get; set; }
		[Parameter("address", "to", 2)]
		public virtual string To { get; set; }
		[Parameter("uint256", "tokenId", 3)]
		public virtual BigInteger TokenId { get; set; }
	}

	public partial class SafeTransferFrom1Function : SafeTransferFrom1FunctionBase { }

	[Function("safeTransferFrom")]
	public class SafeTransferFrom1FunctionBase : FunctionMessage
	{
		[Parameter("address", "from", 1)]
		public virtual string From { get; set; }
		[Parameter("address", "to", 2)]
		public virtual string To { get; set; }
		[Parameter("uint256", "tokenId", 3)]
		public virtual BigInteger TokenId { get; set; }
		[Parameter("bytes", "_data", 4)]
		public virtual byte[] Data { get; set; }
	}

	public partial class SetApprovalForAllFunction : SetApprovalForAllFunctionBase { }

	[Function("setApprovalForAll")]
	public class SetApprovalForAllFunctionBase : FunctionMessage
	{
		[Parameter("address", "operator", 1)]
		public virtual string Operator { get; set; }
		[Parameter("bool", "approved", 2)]
		public virtual bool Approved { get; set; }
	}

	public partial class SupportsInterfaceFunction : SupportsInterfaceFunctionBase { }

	[Function("supportsInterface", "bool")]
	public class SupportsInterfaceFunctionBase : FunctionMessage
	{
		[Parameter("bytes4", "interfaceId", 1)]
		public virtual byte[] InterfaceId { get; set; }
	}

	public partial class SymbolFunction : SymbolFunctionBase { }

	[Function("symbol", "string")]
	public class SymbolFunctionBase : FunctionMessage
	{

	}

	public partial class TokenByIndexFunction : TokenByIndexFunctionBase { }

	[Function("tokenByIndex", "uint256")]
	public class TokenByIndexFunctionBase : FunctionMessage
	{
		[Parameter("uint256", "index", 1)]
		public virtual BigInteger Index { get; set; }
	}

	public partial class TokenOfOwnerByIndexFunction : TokenOfOwnerByIndexFunctionBase { }

	[Function("tokenOfOwnerByIndex", "uint256")]
	public class TokenOfOwnerByIndexFunctionBase : FunctionMessage
	{
		[Parameter("address", "owner", 1)]
		public virtual string Owner { get; set; }
		[Parameter("uint256", "index", 2)]
		public virtual BigInteger Index { get; set; }
	}

	public partial class TokenURIFunction : TokenURIFunctionBase { }

	[Function("tokenURI", "string")]
	public class TokenURIFunctionBase : FunctionMessage
	{
		[Parameter("uint256", "tokenId", 1)]
		public virtual BigInteger TokenId { get; set; }
	}

	public partial class TotalSupplyFunction : TotalSupplyFunctionBase { }

	[Function("totalSupply", "uint256")]
	public class TotalSupplyFunctionBase : FunctionMessage
	{

	}

	public partial class TransferFromFunction : TransferFromFunctionBase { }

	[Function("transferFrom")]
	public class TransferFromFunctionBase : FunctionMessage
	{
		[Parameter("address", "from", 1)]
		public virtual string From { get; set; }
		[Parameter("address", "to", 2)]
		public virtual string To { get; set; }
		[Parameter("uint256", "tokenId", 3)]
		public virtual BigInteger TokenId { get; set; }
	}

	public partial class UnpauseFunction : UnpauseFunctionBase { }

	[Function("unpause")]
	public class UnpauseFunctionBase : FunctionMessage
	{

	}

	public partial class UpdateHeroFunction : UpdateHeroFunctionBase { }

	[Function("updateHero")]
	public class UpdateHeroFunctionBase : FunctionMessage
	{
		[Parameter("tuple", "_hero", 1)]
		public virtual Hero Hero { get; set; }
	}

	public partial class ApprovalEventDTO : ApprovalEventDTOBase { }

	[Event("Approval")]
	public class ApprovalEventDTOBase : IEventDTO
	{
		[Parameter("address", "owner", 1, true)]
		public virtual string Owner { get; set; }
		[Parameter("address", "approved", 2, true)]
		public virtual string Approved { get; set; }
		[Parameter("uint256", "tokenId", 3, true)]
		public virtual BigInteger TokenId { get; set; }
	}

	public partial class ApprovalForAllEventDTO : ApprovalForAllEventDTOBase { }

	[Event("ApprovalForAll")]
	public class ApprovalForAllEventDTOBase : IEventDTO
	{
		[Parameter("address", "owner", 1, true)]
		public virtual string Owner { get; set; }
		[Parameter("address", "operator", 2, true)]
		public virtual string Operator { get; set; }
		[Parameter("bool", "approved", 3, false)]
		public virtual bool Approved { get; set; }
	}

	public partial class HeroSummonedEventDTO : HeroSummonedEventDTOBase { }

	[Event("HeroSummoned")]
	public class HeroSummonedEventDTOBase : IEventDTO
	{
		[Parameter("address", "owner", 1, true)]
		public virtual string Owner { get; set; }
		[Parameter("uint256", "heroId", 2, false)]
		public virtual BigInteger HeroId { get; set; }
		[Parameter("uint256", "summonerId", 3, false)]
		public virtual BigInteger SummonerId { get; set; }
		[Parameter("uint256", "assistantId", 4, false)]
		public virtual BigInteger AssistantId { get; set; }
		[Parameter("uint256", "statGenes", 5, false)]
		public virtual BigInteger StatGenes { get; set; }
		[Parameter("uint256", "visualGenes", 6, false)]
		public virtual BigInteger VisualGenes { get; set; }
	}

	public partial class HeroUpdatedEventDTO : HeroUpdatedEventDTOBase { }

	[Event("HeroUpdated")]
	public class HeroUpdatedEventDTOBase : IEventDTO
	{
		[Parameter("address", "owner", 1, true)]
		public virtual string Owner { get; set; }
		[Parameter("uint256", "heroId", 2, false)]
		public virtual BigInteger HeroId { get; set; }
		[Parameter("tuple", "hero", 3, false)]
		public virtual Hero Hero { get; set; }
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

	public partial class TransferEventDTO : TransferEventDTOBase { }

	[Event("Transfer")]
	public class TransferEventDTOBase : IEventDTO
	{
		[Parameter("address", "from", 1, true)]
		public virtual string From { get; set; }
		[Parameter("address", "to", 2, true)]
		public virtual string To { get; set; }
		[Parameter("uint256", "tokenId", 3, true)]
		public virtual BigInteger TokenId { get; set; }
	}

	public partial class UnpausedEventDTO : UnpausedEventDTOBase { }

	[Event("Unpaused")]
	public class UnpausedEventDTOBase : IEventDTO
	{
		[Parameter("address", "account", 1, false)]
		public virtual string Account { get; set; }
	}

	public partial class BridgeRoleOutputDTO : BridgeRoleOutputDTOBase { }

	[FunctionOutput]
	public class BridgeRoleOutputDTOBase : IFunctionOutputDTO
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

	public partial class HeroModeratorRoleOutputDTO : HeroModeratorRoleOutputDTOBase { }

	[FunctionOutput]
	public class HeroModeratorRoleOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("bytes32", "", 1)]
		public virtual byte[] ReturnValue1 { get; set; }
	}

	public partial class MinterRoleOutputDTO : MinterRoleOutputDTOBase { }

	[FunctionOutput]
	public class MinterRoleOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("bytes32", "", 1)]
		public virtual byte[] ReturnValue1 { get; set; }
	}

	public partial class ModeratorRoleOutputDTO : ModeratorRoleOutputDTOBase { }

	[FunctionOutput]
	public class ModeratorRoleOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("bytes32", "", 1)]
		public virtual byte[] ReturnValue1 { get; set; }
	}



	public partial class BalanceOfOutputDTO : BalanceOfOutputDTOBase { }

	[FunctionOutput]
	public class BalanceOfOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("uint256", "", 1)]
		public virtual BigInteger ReturnValue1 { get; set; }
	}





	public partial class GetApprovedOutputDTO : GetApprovedOutputDTOBase { }

	[FunctionOutput]
	public class GetApprovedOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("address", "", 1)]
		public virtual string ReturnValue1 { get; set; }
	}

	public partial class GetHeroOutputDTO : GetHeroOutputDTOBase { }

	[FunctionOutput]
	public class GetHeroOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("tuple", "", 1)]
		public virtual Hero ReturnValue1 { get; set; }
	}

	public partial class GetRoleAdminOutputDTO : GetRoleAdminOutputDTOBase { }

	[FunctionOutput]
	public class GetRoleAdminOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("bytes32", "", 1)]
		public virtual byte[] ReturnValue1 { get; set; }
	}

	public partial class GetUserHeroesOutputDTO : GetUserHeroesOutputDTOBase { }

	[FunctionOutput]
	public class GetUserHeroesOutputDTOBase : IFunctionOutputDTO
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

	public partial class HeroesOutputDTO : HeroesOutputDTOBase { }

	[FunctionOutput]
	public class HeroesOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("uint256", "id", 1)]
		public virtual BigInteger Id { get; set; }
		[Parameter("tuple", "summoningInfo", 2)]
		public virtual SummoningInfo SummoningInfo { get; set; }
		[Parameter("tuple", "info", 3)]
		public virtual HeroInfo Info { get; set; }
		[Parameter("tuple", "state", 4)]
		public virtual HeroState State { get; set; }
		[Parameter("tuple", "stats", 5)]
		public virtual HeroStats Stats { get; set; }
		[Parameter("tuple", "primaryStatGrowth", 6)]
		public virtual HeroStatGrowth PrimaryStatGrowth { get; set; }
		[Parameter("tuple", "secondaryStatGrowth", 7)]
		public virtual HeroStatGrowth SecondaryStatGrowth { get; set; }
		[Parameter("tuple", "professions", 8)]
		public virtual HeroProfessions Professions { get; set; }
	}



	public partial class IsApprovedForAllOutputDTO : IsApprovedForAllOutputDTOBase { }

	[FunctionOutput]
	public class IsApprovedForAllOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("bool", "", 1)]
		public virtual bool ReturnValue1 { get; set; }
	}

	public partial class NameOutputDTO : NameOutputDTOBase { }

	[FunctionOutput]
	public class NameOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("string", "", 1)]
		public virtual string ReturnValue1 { get; set; }
	}

	public partial class NextHeroIdOutputDTO : NextHeroIdOutputDTOBase { }

	[FunctionOutput]
	public class NextHeroIdOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("uint256", "", 1)]
		public virtual BigInteger ReturnValue1 { get; set; }
	}

	public partial class OwnerOfOutputDTO : OwnerOfOutputDTOBase { }

	[FunctionOutput]
	public class OwnerOfOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("address", "", 1)]
		public virtual string ReturnValue1 { get; set; }
	}



	public partial class PausedOutputDTO : PausedOutputDTOBase { }

	[FunctionOutput]
	public class PausedOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("bool", "", 1)]
		public virtual bool ReturnValue1 { get; set; }
	}











	public partial class SupportsInterfaceOutputDTO : SupportsInterfaceOutputDTOBase { }

	[FunctionOutput]
	public class SupportsInterfaceOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("bool", "", 1)]
		public virtual bool ReturnValue1 { get; set; }
	}

	public partial class SymbolOutputDTO : SymbolOutputDTOBase { }

	[FunctionOutput]
	public class SymbolOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("string", "", 1)]
		public virtual string ReturnValue1 { get; set; }
	}

	public partial class TokenByIndexOutputDTO : TokenByIndexOutputDTOBase { }

	[FunctionOutput]
	public class TokenByIndexOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("uint256", "", 1)]
		public virtual BigInteger ReturnValue1 { get; set; }
	}

	public partial class TokenOfOwnerByIndexOutputDTO : TokenOfOwnerByIndexOutputDTOBase { }

	[FunctionOutput]
	public class TokenOfOwnerByIndexOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("uint256", "", 1)]
		public virtual BigInteger ReturnValue1 { get; set; }
	}

	public partial class TokenURIOutputDTO : TokenURIOutputDTOBase { }

	[FunctionOutput]
	public class TokenURIOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("string", "", 1)]
		public virtual string ReturnValue1 { get; set; }
	}

	public partial class TotalSupplyOutputDTO : TotalSupplyOutputDTOBase { }

	[FunctionOutput]
	public class TotalSupplyOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("uint256", "", 1)]
		public virtual BigInteger ReturnValue1 { get; set; }
	}






}
