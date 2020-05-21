namespace Confy.Git.Interfaces
{
    internal interface IPlatformRepository
    {
        string Clone(GitConfigurationSource source);
    }
}