## Code
- All code added should have automated test when possible(we use MSTest), excluding two case:
    - The code is necessarily coupled with Godot, such as godot scene changing.
    - The code is a single assignment or return statement with no possible branching or logic e.g. you don't really need to test that *return 1;* returns 1.
- Code against contracts, not concrete types. e.g. specify variables or parameters as *ICard* not *RCard*.
- Abide by the general coding best practice, Don't Repeat Yourself, things should have single resonsibilities, etc.
- Prefer C# events over using godot signals.
- Don't hard code node paths, use an *Export* attribute of a field.

## Nameing convention
- Use *camelCase* for fields, variables, or parameters.
- Use *PascalCase* for types, methods, or properties.
- Name all scenes in *snake_case*, and place them in the *scenes* directory.
- Only one public C# type per file, file and type should have same name.
- Prefix all C# types with **one** of the following indicators, preferring the one's higher in the list.
    - Test: the type's marked with the **[TestClass]** attrbute
    - Fake: the type's a fake/mock/stub/etc
    - N: the type extends Godot.Node
    - S: the type is marked static
    - I: the type is an interface
    - R: the type is a class
    - V: the type is a struct
    - E: the type is an enum
- Don't start test method names with "Test".
- Start private field names with an underscore(_), or 's_' when the field is static.

## Git

- Always create a pull requests, never merge directly into main.
- Never merge a pull request in which you're the author of the most recent commit, unless somebody else approves it.
- Give pull requests a name that describes the feature that is being added.
- Write commit messages as imperatives. e.g. "Update class X" not "Updated class X".
- Name all branches as descriptive of the feature that they're adding.
- Only develop one feature or change on each branch.

