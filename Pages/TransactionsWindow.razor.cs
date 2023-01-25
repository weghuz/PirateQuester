using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PirateQuester.Utils;

namespace PirateQuester.Pages;

public partial class TransactionsWindow
{
	[Inject]
	public AccountManager Acc { get; set; }
	[Inject]
	public NavigationManager Nav { get; set; }
	[Inject]
	public IJSInProcessRuntime JS { get; set; }

	public TransactionsWindow()
	{

	}
}