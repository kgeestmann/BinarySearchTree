# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

ADS ("Algorithmen und Datenstrukturen") is a university course project in C# / .NET 9 console app. It contains hand-written implementations of a binary search tree, a max/min priority queue with changeable keys, and a graph with BFS, DFS and Dijkstra. The code comments are in **German**, so keep it that way. The algorithms deliberately follow the lecture pseudocode (node colours, `Parent`/`Distance` fields). Don't replace them with library collections or "smarter" variants.

## Commands

```bash
dotnet build ADS.sln
dotnet run --project ADS       # runs the demo in ADS/Program.cs
```

There is no test project yet. `ADS/Program.cs` is the demo and the manual check: it prints the expected values next to the actual output (e.g. "soll 10 sein", German for "should be 10"). The build currently produces a few nullable warnings (CS8625).

## Architecture

- **`IElementWithKey`** is the contract that the tree and the queue share. `Key` is fixed and orders the BST. `ChangeableKey` orders the priority queue. `ChangingAction` is a callback that the queue injects into each element.
- **Changeable-key mechanism:** when you insert an element into `MaxMinPriorityQueue<T>`, the queue sets the element's `ChangingAction`. Setting `ChangeableKey` on the element then calls back into the queue, which restores the heap property right away. `Models/Test.cs` shows how an element must implement this: a setter that calls `_changingAction()`.
- **`DataStructures/MinMaxPriorityQueue.cs`** is an array-based binary heap. A `MaxMin` mode selects the comparison. `MaxPriorityQueue<T>` and `MinPriorityQueue<T>` are thin subclasses that only fix the mode.
- **`DataStructures/BinarySearchTree.cs`** and **`BinaryNode.cs`** implement a BST keyed by `Key`. It supports `Insert`, `Find`, `Delete`, `TreeMinimum` and in-order traversal with a callback. Missing keys throw exceptions.
- **`Graph/`** has a `Graph` with nodes stored in an array indexed by id. Node 0 is unused in the demo. `Node` carries the traversal state (`Color`, `Distance`, `Parent`), and `Edge` stores a weight. `BFS`, `DFS` and `Dijkstra` write their results into that node state, and `PrintPath` follows the `Parent` links. Dijkstra uses the priority queue from `DataStructures/`.

## Keeping docs in sync

There is no README yet. The XML and inline comments are the documentation. If you change behaviour, update the comments and the demo output in `Program.cs` **in the same PR**.

## Filing issues

If you find a bug, a gap or a docs/code mismatch outside the current task, **file it right away** with `gh issue create`. Don't silently fix it inside an unrelated change. Include the `file:line`, a concrete failure scenario and the expected behaviour. Mention the issue number in your summary or PR description.

## Git & PR workflow

Every change (feature, fix or docs) starts from a GitHub issue and gets its own branch and PR:

1. **Issue:** use the existing one, or create it first with `gh issue create`. One issue = one branch = one PR.
2. **Branch from the issue:**
   ```bash
   gh issue develop <n> --name <type>/<n>-short-description --base master --checkout
   ```
   This links the branch under the issue's *Development* section. `<type>` is the Conventional Commit type (`feat`, `fix`, `docs`, …). Never commit directly to `master`.
3. **Commit** using Conventional Commits.
4. **Before the PR:** `dotnet build ADS.sln` must succeed, and `dotnet run --project ADS` must still print the expected values.
5. **PR:** put `Closes #<n>` in the description so the merge closes the issue and links it to the PR.
6. **Review and merge:** the `claude-code-review` workflow reviews each PR automatically. For an on-demand review or a question, write `@claude` in a PR or issue comment. Act on valid findings, then merge with `gh pr merge <n> --merge --delete-branch`.

No confirmation is needed in this repo to commit, push, open or merge your own PRs (approved by the owner). Force-push and history rewrites still need explicit approval.

Gotchas for the review workflow:

- It does not review PRs that change `.github/workflows/claude*.yml`, because the action only runs when the workflow file matches `master`.
- It skips PRs that Claude has already commented on. Use `@claude` to get a re-review.
