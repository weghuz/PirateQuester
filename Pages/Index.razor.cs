using DFK;
using PirateQuester.DFK.Contracts;
using PirateQuester.Utils;
using Radzen.Blazor;
using Radzen;
using System.Text;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Utils;

namespace PirateQuester.Pages;

public partial class Index
{

	[Inject]
	public AccountManager Acc { get; set; }
	[Inject]
	public NavigationManager Nav { get; set; }
	[Inject]
	public IJSInProcessRuntime JS { get; set; }
	[Inject]
	public DialogService Dialog { get; set; }
	[Inject]
	public GlobalState Global { get; set; }
	[Inject]
	public TransactionManager Transact { get; set; }


    public List<string> SelectedOwners { get; set; } = new();
    public List<Hero> TableHeroes { get; set; } = new();

    string pagingSummaryFormat = "Displaying page {0} of {1} (total {2} records)";
	bool Loading = false;
	string ownerInput = "";
	IList<Hero> SelectedHeroes;
	RadzenDataGrid<Hero> heroes;
	ButtonStyle StartQuestStyle = ButtonStyle.Secondary;
	bool StartQuestButtonDisabled = true;
	string Attempts = "1";

	private async Task CompleteQuest()
	{
		StringBuilder output = new();
		DFKAccount acc = Acc.Accounts.FirstOrDefault();
		try
		{
			int skip = 0;
			while (skip <= SelectedHeroes.Count)
			{
				List<Hero> heroes = SelectedHeroes.ToList().Skip(skip).Take(6).ToList();
				var transactionReceipt = await Transact.CompleteQuest(Acc.Accounts.FirstOrDefault(), heroes.ToList());
				Console.WriteLine($"{transactionReceipt.Status}: {string.Join(", ", transactionReceipt.Logs)}");
				output.AppendLine($"Completed fishing for: {string.Join(", ", heroes)}");
			}
		}
		catch (Exception e)
		{
			Console.WriteLine($"{e.Source} - {e.Message} - {e.InnerException?.Message}");
			output.AppendLine(e.Message);
		}
	}

	private async Task StartFishing()
	{
		StringBuilder output = new();
		DFKAccount acc = Acc.Accounts.FirstOrDefault();
		try
		{
			int skip = 0;
			while (skip <= SelectedHeroes.Count)
			{
				List<Hero> heroes = SelectedHeroes.ToList().Skip(skip).Take(6).ToList();
				var transactionReceipt = await Transact.StartQuest(acc, heroes, QuestType.FISHING, int.Parse(Attempts));
				Console.WriteLine($"{transactionReceipt.Status}: {string.Join(", ", transactionReceipt.Logs)}");
				output.AppendLine($"Started fishing for: {string.Join(", ", heroes)}");
			}
		}
		catch (Exception e)
		{
			Console.WriteLine($"{e.Source} - {e.Message} - {e.InnerException?.Message}");
			output.AppendLine(e.Message);
		}
		OpenDialog(output.ToString());
	}

	private void UpdateSelection()
	{
		if (heroes.Count > 0)
		{
			StartQuestButtonDisabled = false;
			StartQuestStyle = ButtonStyle.Success;
		}
		else
		{
			StartQuestButtonDisabled = true;
			StartQuestStyle = ButtonStyle.Warning;
		}
	}

	protected override async Task OnInitializedAsync()
	{
		SelectedOwners = Acc.Accounts.Select(acc => acc.Account.Address).ToList();
		await LoadHeroes();
	}

	private async Task LoadHeroes()
	{
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
			Console.WriteLine(TableHeroes);
			Loading = false;
		}
	}
}
