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

    public List<string> SelectedOwners { get; set; } = new();
    public List<Hero> TableHeroes { get; set; } = new();

    public List<string> SelectedQuestName { get; set; }

    string pagingSummaryFormat = "Displaying page {0} of {1} (total {2} records)";
	bool Loading = false;
	string ownerInput = "";
	IList<Hero> SelectedHeroes = new List<Hero>();
	RadzenDataGrid<Hero> heroes;


	ButtonStyle StartQuestStyle = ButtonStyle.Secondary;
	bool StartQuestButtonDisabled = true;
	string Attempts = "1";

	private void ChangeQuestSelection()
	{
		SelectedQuestName = SelectedQuestName.Take(1).ToList();
	}

	private async Task UpdateHeroes()
	{
		foreach(DFKAccount acc in  Acc.Accounts)
		{
			await acc.UpdateHeroes();
		}
		await LoadHeroes();
	}

	private async Task CompleteQuest()
	{
		DialogWindow("Completing Quest...");
		StringBuilder output = new();
		DFKAccount acc = Acc.Accounts.FirstOrDefault();
		if(SelectedHeroes.Count <= 0)
		{
			Console.WriteLine($"Select Heroes!");
            DialogWindow("Select Heroes!");
            return;
		}
		try
		{
			string okMessage = await new Transaction().CompleteQuest(Acc.Accounts.FirstOrDefault(), SelectedHeroes.FirstOrDefault());
			Console.WriteLine($"{okMessage}");
			output.AppendLine($"Completed Quest: {okMessage}");
		}
		catch (Exception e)
		{
			Console.WriteLine($"{e.Message}");
			output.AppendLine(e.Message);
		}
		Dialog.Close();
		DialogWindow("Completed Quest", output.ToString());
	}

	private async Task StartQuest()
	{
		QuestContract SelectedQuest = ContractDefinitions.GetQuestContract(SelectedQuestName.FirstOrDefault());
		if (SelectedQuest is null)
		{
			DialogWindow($"Select a quest!");
			return;
		}
		DialogWindow($"Starting {SelectedQuest.Name}...");
		StringBuilder output = new();
		DFKAccount acc = Acc.Accounts.FirstOrDefault();
		try
		{
			List<Hero> heroes = SelectedHeroes.ToList();
			Console.WriteLine(SelectedHeroes.ToString());
			string okMessage = await new Transaction().StartQuest(acc, heroes.ConvertAll<BigInteger>(h => long.Parse(h.id)), (QuestType)SelectedQuest.Id, int.Parse(Attempts));
			foreach(Hero h in heroes)
			{
				h.staminaFullAt -= 1200 * int.Parse(Attempts);
			}
			Console.WriteLine($"{okMessage}");
			output.AppendLine($"Started {SelectedQuest.Name}: {okMessage}");
		}
		catch (Exception e)
		{
			Console.WriteLine($"{e.Message}");
			output.AppendLine(e.Message);
		}
		Dialog.Close();
		DialogWindow($"Started {SelectedQuest.Name}", output.ToString());
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
