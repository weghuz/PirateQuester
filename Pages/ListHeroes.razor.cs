using DFK;
using PirateQuester.DFK.Contracts;
using PirateQuester.Utils;
using Radzen.Blazor;
using Radzen;
using System.Text;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Utils;
using System.Numerics;

namespace PirateQuester.Pages;

public partial class ListHeroes
{

	[Inject]
	public AccountManager Acc { get; set; }
	[Inject]
	public NavigationManager Nav { get; set; }
	[Inject]
	public IJSInProcessRuntime JS { get; set; }
	[Inject]
	public DialogService Dialog { get; set; }

    public List<string> SelectedOwners { get; set; } = new();
    public List<Hero> TableHeroes { get; set; } = new();

    public string SelectedQuestName { get; set; }

    string pagingSummaryFormat = "Displaying page {0} of {1} (total {2} records)";
	bool Loading = false;
	string ownerInput = "";
	IList<Hero> SelectedHeroes = new List<Hero>();
	RadzenDataGrid<Hero> heroes;

	string Attempts = "1";

	private async Task UpdateHeroes()
	{
		foreach(DFKAccount acc in  Acc.Accounts)
		{
			await acc.UpdateHeroes();
		}
		await LoadHeroes();
	}

	protected override async Task OnInitializedAsync()
	{
		SelectedOwners = Acc.Accounts.Select(acc => acc.Account.Address).ToList();
		await LoadHeroes();
	}

	private async Task LoadHeroes()
	{
		SelectedHeroes = new List<Hero>();
		if (SelectedOwners is null)
		{
			return;
		}
		List<string> ownerArg = SelectedOwners;
		if (ownerInput != "")
		{
			string[] inputOwners = ownerInput.Split(new string[] { ",", "\r\n", "\n", "\r" }, StringSplitOptions.TrimEntries);
			foreach (string o in inputOwners)
			{
				if (o.Length == 42 && o.StartsWith("0x"))
				{
					ownerArg.Add(o);
				}
			}
		}
		if (ownerArg.Count > 0)
		{
			Loading = true;
			Dictionary<HeroesArgument, string> args = new();
			args.Add(HeroesArgument.owner_in, $@"[""{string.Join(@""",""", ownerArg)}""]");
			string request = API.HeroesRequestBuilder(args);
			TableHeroes = (await API.GetHeroes(request)).ToList();
			Loading = false;
		}
	}
}
