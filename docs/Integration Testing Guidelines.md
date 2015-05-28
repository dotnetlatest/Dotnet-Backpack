##### Integration Testing Guidelines

How do I test interacting with an external API that effects live data?
You don't. You have to actually trust that the actual API actually works.

You can -- and should -- exercise the API with live data to be sure you understand it.

But you don't need to test it. If the API doesn't work, simply stop using it. Don't test every edge and corner case.

How can I mock / stub objects in an Integration test when they're hidden behind layers of abstraction?
That's the point. Test the abstraction. You have to trust that the implementation works. You're testing your code. Not their code.

What do I do when a test fails and the live data is left in an inconsistent state?
What? Why are you testing live API's to be sure they work? You don't trust them? If you don't trust them, don't test. Find a vendor you can trust.

You only test your code. You trust their code. You trivially mock enough of their code to be sure your code works.