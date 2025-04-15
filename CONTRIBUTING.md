# Contributing

This project welcomes contributions and suggestions. Here are the guidelines to follow:
- [Question or Problem?](#question)
- [Report a Bug](#issue)
- [Request a Feature](#issue)
- [Pull Request](#pull-request)
- [Coding Rules](#rules)
- [Commit Message](#commit)

## <a name="question"></a> Got a Question or Problem?

If you have a question or need help, you can ask in the discussion tab.

> Please, do not open issues for general support questions as we want to keep issues for bug reports and feature requests.

## <a name="issue"></a> Report a bug or request a Feature

You can open a issue to report a bug or request a new feature. Before, it is best to research whether a similar issue already exists. If a similar issue exists, you can upvote it with 👍 to show your interest. This may have an impact on the prioritization of their treatment.

## <a name="pull-request"></a> Submitting a Pull Request

You can submit a **pull request** by following this guidelines:

1. Submit a issue by following [the above guidelines](#issue). Discussing upfront helps to ensure that the fix/feature can be accepted.
2. [Fork](https://docs.github.com/en/github/getting-started-with-github/fork-a-repo) the repository.
3. In the forked repository, make the changes in a new git branch
4. Modify the code, **including appropriate test cases**.
5. Follow our [Coding Rules](#rules).
6. Create a pull request to the branch `main`.
7. The team will review the pull request and merge.
8. A huge thank you for your participation.


## <a name="rules"></a> Coding Rules

The coding rules are available in "*[.editorconfig](.editorconfig)*" and enforced by [Roslyn](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/overview). 

## <a name="commit"></a> Commit Message

> These guidelines are only for commits on the main branches. Commits on development branches don't need to conform to the following guidelines.

Commit messages follow [Conventional Commit](https://www.conventionalcommits.org). They should have the following format:
```
<type>(<scope>): <short summary>
<long description>(optional)
<BLANK LINE>
<metadata>
```

**type**

The type define the type of change. It must be one of the following:

| Type      | Description                                       |
|-----------|---------------------------------------------------|
| **feat**  | A new feature                                     |
| **fix**   | A bug fix                                         |
| **chore** | A chore (refactoring, update dependency, ...)     |

**scope**

The scope define what is changed. It must be one of the following:

| Scope     | Description                                         |
|-----------|-----------------------------------------------------|
| **lib**   | The source code (library, test, documentation, ...) |
| **ci**    | CI/CD (Github Actions, SonarQube, ...)              |
| **\***    | A little bit of everything                          |

**short summary**

A short summary to describe promptly the modification in 100 characters maximum and in present tense without period at the end.

**long description** (optional)

A long description to describe explicitly the modification whitout size limit and in markdown. The long description isn't necessary if the short description is sufficient

**metadata**

Many metadata in format `<key>: <value>`.

Example :
```
feat(lib): Add DictionaryJsonConverter
Add the class `DictionaryJsonConverter` to deserialize `Dictionary<string, object>`.

GitHub PR: #4
GitHub Issue: #3
```
