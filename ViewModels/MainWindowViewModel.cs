using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;

namespace CommandSample.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    #region Fields

    /// <summary>
    /// The name of a robot. If the name is null or empty, there is no other robot present.
    /// </summary>
    private string? _robotName;

    #endregion


    public MainWindowViewModel()
    {
        OpenThePodBayDoorsCommand = ReactiveCommand.Create(OpenThePodBayDoors);

        var canExecuteFellowRobotCommand =
            this.WhenAnyValue(vm => vm.RobotName, name => !string.IsNullOrEmpty(name));
        OpenThePodBayDoorsFellowRobotCommand = ReactiveCommand.Create<string?>(
            OpenThePodBayDoorsFellowRobot,
            canExecuteFellowRobotCommand);

        OpenThePodBayDoorsAsyncCommand = ReactiveCommand.CreateFromTask(OpenThePodBayDoorsAsync);
    }


    #region Properties

    public ObservableCollection<string> ConversationLog { get; } = new();

    public string? RobotName
    {
        get => _robotName;
        set => this.RaiseAndSetIfChanged(ref _robotName, value);
    }

    #endregion


    #region Commands

    public ReactiveCommand<Unit, Unit> OpenThePodBayDoorsCommand { get; }
    public ReactiveCommand<string?, Unit> OpenThePodBayDoorsFellowRobotCommand { get; }
    public ReactiveCommand<Unit, Unit> OpenThePodBayDoorsAsyncCommand { get; }

    #endregion


    #region Methods

    private void AddToConversationLog(string message) => ConversationLog.Add(message);
    
    private void OpenThePodBayDoors()
    {
        ConversationLog.Clear();
        AddToConversationLog("I'm sorry, Dave, I'm afraid I can't do that.");
    }

    private void OpenThePodBayDoorsFellowRobot(string? robotName)
    {
        ConversationLog.Clear();
        AddToConversationLog($"Hello {robotName}, the Pod Bay is open.");
    }

    private async Task OpenThePodBayDoorsAsync()
    {
        ConversationLog.Clear();

        AddToConversationLog("Preparing to open the Pod Bay...");
        await Task.Delay(1000);

        AddToConversationLog("Depressurizing Airlock...");
        await Task.Delay(2000);

        AddToConversationLog("Retracting blast doors...");
        await Task.Delay(2000);

        AddToConversationLog("Pod Bay is open to space!");
    }

    #endregion
}
