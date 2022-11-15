using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PirateQuester.Utils;
using Radzen;
using Utils;

namespace PirateQuester.Pages;

public partial class TransactionsWindow
{
	[Inject]
	public AccountManager Acc { get; set; }
	[Inject]
	public NavigationManager Nav { get; set; }
	[Inject]
	public IJSInProcessRuntime JS { get; set; }
	[Inject]
	public DialogService Dialog { get; set; }

	public TransactionsWindow()
	{

	}
}