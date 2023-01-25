using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using PirateQuester.Services;
using PirateQuester.Utils;
using PirateQuester.ViewModels;

namespace PirateQuester.Pages;

public partial class CreateAccount
{
	[Inject]
	AccountManager Acc { get; set; }
	[Inject]
	NavigationManager Nav { get; set; }
	[Inject]
	BotService Bots { get; set; }
	[Inject]
	IJSInProcessRuntime JS { get; set; }
	public bool Creating { get; set; }
	public static string ShowingState { get; set; } = "create";
	AccountViewModel createModel { get; set; } = new();
	ImportAccountViewModel importModel { get; set; } = new();

	private async Task OnInputFileChange(InputFileChangeEventArgs e)
	{
		foreach (var file in e.GetMultipleFiles(1))
		{
			try
			{
				var fileContent = new StreamContent(file.OpenReadStream(file.Size));
				importModel.UploadedAccount = await fileContent.ReadAsStringAsync();
				Console.WriteLine(importModel.UploadedAccount);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}

	void ImportAccount()
	{
		if (Acc.ImportAccount(importModel))
		{
			JS.InvokeVoid("alert", "Account uploaded, go to Login to log in.");
			importModel = new();
			StateHasChanged();
		}
	}

	void ShowCreateAccount()
	{
		ShowingState = "create";
	}

	void ShowImportAccount()
	{
		ShowingState = "import";
	}

	async Task Create()
	{
		Creating = true;
		await Acc.Create(createModel, Bots.Settings);
		Nav.NavigateTo("Accounts");
	}
}