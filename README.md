# Testing Mastery Exercises

Associate exercises for Eficode's "Testing Mastery" training. 

This exercise uses a C# backend with a React Typescript frontend. Therefore you need: 

* NodeJS: https://nodejs.org/en/
* Visual studio: https://visualstudio.microsoft.com/ 
  - .net v6.0
  - ASP.net
  - NodeJS

## Unit testing

This exercise is about writing good tests. Provided are nine different versions of the same function. They all look natural, but all have bugs, except for one. Sort of a Where's Waldo but with bugs. The function has a fairly simple purpose:

1. take an array of floats,
2. find the average, and
3. subtract the average from each element.

Construct a test suite to weed out all the bad implementations. Keep in mind:

* A good test, tests something -- ie. fails at least one of the erroneous programs.
* A good test is specific -- ie. fails as few implementations as possible. (Only one of my tests fail more than one implementation.)
* A good test is correct -- ie. never fails the working implementation.
* A good suite should be complete -- ie. fail all but the working implementation.

The solution has 11 test cases, find as many as you can.

Implement your tests in BackendTests/SpecificTestsExercise.cs. Remember the goal is _not_ to make passing tests, but for the tests to fail on as few implementations as possible.

## Test doubles

Good test doubles are the difference between locking the structure or the behavior. We want the behavior to remain the same even when we refactor the structure. Therefore our tests should only `new` one real class. All other `new`s should be test doubles. Finish and fix the test case in `BackendTests/TestDoublesExercise.cs`. You may need to make new classes and interfaces. 

1. Begin by creating a test double that implements SSORegistry, and use that in the test.
2. Fix the unit test, while implementing the nessasary test doubles.

## Contract testing

Sometimes a change to a 3rd party service can crash our application. To quickly detect when this happens we use contract tests. These simply make a call to the external service and verify that the response have the correct structure. 

1. Make a contract test for the dad-joke service, in the file `BackendTests/ContractTests.cs`.

<details>
  <summary>Help!</summary>

Take inspiration from `Backend/Outgoing/DadJoke.cs`.
</details>

## UI testing

Some functionality is required for our application to be usable. To verify this we can use UI testing. UI tests make a virtual rendering of our app, so we can query and check elements. 

1. Make a UI test to verify that our UI contains a link that says "learn react".

<details>
  <summary>Help!</summary>

Create a file called `App.test.tsx`, containing:

```
import { render, screen } from "@testing-library/react";
import App from "./App";

test("renders learn react link", () => {
  render(<App />);
  const linkElement = screen.getByText(/learn react/i);
  expect(linkElement).toBeInTheDocument();
});
```

</details>

2. Can you make the test fail?

## Approval testing

We currently have no tests of our custom `FunnyComponent`. To quickly -- but shallowly -- remedy this we can use approval testing. We first install a library:

```
npm install react-test-renderer
npm install @types/react-test-renderer
npm install react-renderer-test@17.0.2
```

Create a file called `FunnyComponent.test.tsx`, containing:

```
import renderer from "react-test-renderer";
import { FunnyComponent } from "./FunnyComponent";

test("renders correctly", () => {
  const tree = renderer.create(<FunnyComponent></FunnyComponent>).toJSON();
  expect(tree).toMatchSnapshot();
});
```

1. Inspect the snapshot. Can you make this test fail?

## Coverage and mutation testing

### Test coverage

Confidence is the fundamental reason we do testing. One method to assess how much confidence we should have in our test suite is to measure test coverage. Luckily this is trivial to do in most frameworks, including jest which our frontend is using. 
1. Add a run configuration in `package.json`, for running the tests with the `--coverage` flag. 
2. Then use it to find the coverage of the two important files: 
  * App.tsx
  * FunnyComponent.tsx

<details>
  <summary>Help!</summary>

1. Put this in package.json.  

```
  ...
  "scripts": {
    "test": "react-scripts test",
    "coverage": "react-scripts test --coverage",
    ...
```

2. Then run the command `npm run coverage`.
</details>

### Mutation testing

As our coverage is pretty high we need to use a stronger tool: mutation. To set this up we install a tool called stryker:

```
npm i -D @stryker-mutator/core @stryker-mutator/jest-runner @stryker-mutator/typescript-checker
```

We also configure it by creating a file called `stryker.conf.json`:

```
{
  "testRunner": "jest",
  "jest": {
    "projectType": "create-react-app"
  },
  "checkers": ["typescript"],
  "tsconfigFile": "tsconfig.json",
  "tempDirName": "stryker-tmp"
}
```

1. Now we can add another run configuration for it, with the command `stryker run`. 
2. What is the actual coverage of the two files?
  * App.tsx
  * FunnyComponent.tsx

## Resilience testing

If we want to increase the resilience of our application we need to inject chaos. The simplest way to do this is to force our application to fail occasionally. In this exercise we want to motivate retry-logic in the frontend. 

1. Make the `/Joke` endpoint fail 5% of the time.

<details>
  <summary>Help!</summary>

In `Backend/Controllers/JokeController.cs`, modify the `Get`-method to throw an exception if a random float is less than 0.05.

</details>

