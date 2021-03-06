typed-url-routing
=================

This is a library which adds **strong static typing** to the **URL-routing** and **link generation** in Microsoft's **MVC4**.

It lets you:

 * Define your application’s URL routes as _statically-typed, first-class-objects_
 * Bind routes to controller actions, in such a way that the _compiler catches your mistakes_ (like parameter mismatches or misspellings)
 * Generate links in your Razor code (a) _succinctly_ and (b) _checked at compile-time_.
 * Use Visual Studio refactoring to _rename routes or actions, automatically_—without missing any references.

Oh and:

 * It all works at compile time. You don’t need to run a program to generate code or anything like that. Just include the assembly in your project and start using it.
 * You will write less code.

I’ve used it so far in one commercial web project (small intranet system for an energy supplier). It’s already reached the stage where I’d much rather use it than the built-in, dynamically-typed MVC routing system.

Here’s the original blog post which explains the project:
> https://dysphoria.net/2013/03/14/strongly-typed-action-references-in-microsoft-mvc4/

I can’t offer commercial support at the moment, but please get in touch if you’re using it. Do let me know of any problems you have with it, or suggestions. Also, pull requests for improvements are very welcome!

–Andrew Forrest
