public static class TwoFer
{
    public static string Speak(string name = null) => $"One for {(name is null ? "you" : name)}, one for me.";
}
