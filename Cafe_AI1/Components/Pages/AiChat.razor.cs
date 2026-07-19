using Cafe_AI.Services;
using Microsoft.AspNetCore.Components;

namespace Cafe_AI1.Components.Pages;

public partial class AiChat
{
    [Inject] private AiService AiService { get; set; } = default!;

    private string userName = string.Empty;
    private string userRole = string.Empty;
    private string nameInput = string.Empty;
    private string roleInput = string.Empty;
    private List<AiMessage> messages = new();
    private string newMessage = string.Empty;
    private bool isLoading = false;

    private void JoinAiChat()
    {
        if (!string.IsNullOrWhiteSpace(nameInput) && !string.IsNullOrWhiteSpace(roleInput))
        {
            userName = nameInput;
            userRole = roleInput;
        }
    }

    private async Task SendToAi()
    {
        if (string.IsNullOrWhiteSpace(newMessage)) return;

        messages.Add(new AiMessage { Content = newMessage, SenderName = $"{userName} ({userRole})", IsUser = true });
        var question = newMessage;
        newMessage = string.Empty;
        isLoading = true;
        StateHasChanged();

        var response = await AiService.AskAssistantAsync(question, userRole);
        messages.Add(new AiMessage { Content = response, SenderName = "ИИ-ассистент", IsUser = false });

        isLoading = false;
        StateHasChanged();
    }

    private async Task QuickAsk(string question)
    {
        newMessage = question;
        await SendToAi();
    }

    private class AiMessage
    {
        public string Content { get; set; } = "";
        public string SenderName { get; set; } = "";
        public bool IsUser { get; set; }
    }
}