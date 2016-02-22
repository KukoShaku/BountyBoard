# BountyBoard - The world's most active git repository

## Summary

I, Peabnuts123, have pushed a framework project to this repository.
Below, I have documented how it works, and how to use it.

I plan on pushing this framework as a Visual Studio template to github
at some point. I will update this readme if/when that happens.

Enjoy!

## Overview
This is the BBANG JUK Framework:

* [**B**ower](https://github.com/bower/bower)
    * Front-end package manager for libraries that are run and loaded on the front-end e.g. Bootstrap.
* [**B**ootstrap](https://github.com/twbs/bootstrap)
    * Front-end library of pre-built components and stylesheets for web design.
* [**A**ngular](https://github.com/angular/angular.js)
    * Self-proclaimed "super heroic" JavaScript library for easy construction of highly interactive JavaScript-centric web pages.
* [**N**odeJS](https://github.com/nodejs/node)
    * Offline implementation of V8 JavaScript engine for utilisation of JavaScript as a development platform.
* [**G**ulp](https://github.com/gulpjs/gulp)
    * Script runner for automation of tasks.
* [**J**asmine](https://github.com/jasmine/jasmine)
    * BDD-style unit testing framework for JavaScript.
* [**U**nderscore](https://github.com/jashkenas/underscore)
    * Utility library of highly useful and easy-to-use JavaScript functions.
* [**K**arma](https://github.com/karma-runner/karma)
    * Test-running framework that uses a real browser to execute JavaScript tests.

## Requirements
This project requires a few things to be installed in order to operate properly:

* [NodeJS](https://nodejs.org/en/)
    * Needed for package dependency management and running automation scripts / unit tests etc.
    * I would recommend downloading **Latest Stable**
    * Ensure `node` and `npm` end up on your PATH.
* [Git](https://git-scm.com/downloads)
    * Required for `bower`, so that it may download packages.
    * Has added benefit of being able to list **any** GitHub repo as a dependency for your project!
    * Ensure `git` is added to PATH.
* **NodeJS .bin folder must be added to %PATH%**
    * Add `.\node_modules\.bin` to your PATH environment variable.


The following items are recommended but not required to run the project:

* [Karma Test Adaptor](https://github.com/MortenHoustonLudvigsen/KarmaTestAdapter)
    * This is an extension for Visual Studio to integrate Karma's Unit tests to its **Test Runner** window.
    * The tests run and update in real time as you edit code, and you never have to run `karma` by hand, so it's super easy to use.


## Running the Project
Running the project is theoretically easy. Use `git` to clone this repo and open the project in Visual Studio. 
Visual Studio *should* download all required references and, upon building, run the required `gulp` scripts to
set up everything that is needed.

Upon a successful launch of the project, there should be no JavaScript errors in the `console`, no HTTP errors under `network`, 
and a non-empty page displayed on the screen.

See [Troubleshooting](#troubleshooting) if you are unable to get the project running.

## Working with the framework
Currently the framework is set up pretty plainly. It does not abstract any functionality away from **AngularJS**, the framework
being used to build the site, it merely offers support *around* the angular development.

I personally have more code to contribute around various common aspects of developing an
angular application, but I will add it at a later date.

Planned additions:

* Abstraction around Angular Routes

## Unit Testing
#### Background
Unit Tests for the project are written and implemented using the **Jasmine** framework. 
**Karma** is used to run the tests.

Upon running (via command line or Visual Studio extension) Karma will **physically open an instance of Chrome
that you will need to keep open in order for your tests to run**. If you try to close the browser, Karma will
try to re-open it. If you close the browser more than 2 times then Karma will give up and stop running.

Jasmine Tests are implemented via files named `*Tests.js` placed in the `Tests/` folder (e.g. `Tests/fileTests.js`, `Tests/sharePointTests.js` etc.). You can find
pretty comprehensive documentation around how to use Jasmine [on their site](http://jasmine.github.io/edge/introduction.html).


#### Configuration

Karma is configured to depend on a set of files that it includes when it runs its tests.
These files are loaded in the order they are specified, and you can specify a wildcard set of files
e.g. `*.js`. Originally, this was configured to read something like `WebFiles/**/*.js` in order to 
include all JavaScript files in the project. However, this produced issues when it would load files that
depended on angular before angular was loaded, and foretold future issues wherein dependencies would not
be preserved from the web project.

A solution was developed to read all the included JavaScript resources out of `index.html` in order,
and paste the absolute filepaths of these resources into the `karma.conf.json` file. This would ensure
any new resources added to the project would be added to the test project, and any dependencies would be preserved as the
order of loading would be maintained from the web project. This also had the added benefit of ensuring
that the environment of the Test Project matched that of the web project.

#### Running Tests
Either install the aforementioned extension [Karma Test Adapter](https://github.com/MortenHoustonLudvigsen/KarmaTestAdapter) for Visual Studio or run the command `karma start` 
from the command line (whilst in the folder containing `package.json`). You can hit `Ctrl+C` to cancel Karma from the Command Line.

If you install the **Karma Test Adapter** extension, it will run Karma for you and integrate with the Visual Studio Test Runner.

Running tests from the Command Line is more reliable and lightweight, but it means you need to have a command line window open.

## Troubleshooting
There may very well be issues with the project as it stands. I've done my best to outline the most common ones below.
[Open a new Issue](https://github.com/KukoShaku/BountyBoard/issues/new) on GitHub if you are experiencing any issues that are not
listed here.

#### Commands like `gulp`, `bower` or `karma` aren't recognised as a commands, operable programs etc *OR* Task Runner Explorer in Visual Studio says gulp can't load
Ensure the following string is in your path: `.\node_modules\.bin`.

If that fails, delete the folders `node_modules` and `bower_components`, then run `npm install` and then `bower install` from the command line. Then rebuild the project in VS.

#### No seriously, none of my node stuff is running / downloading!
Try deleting `node_modules` and `bower_components`, then ALSO delete `%appdata%/npm-cache` and THEN run `npm install` and `bower install` from the command line.

---

If any bower or node dependencies are added by someone else, you will need to run bower/npm install from the command line. Cleaning out your solution, deleting the `node_modules`/`bower_components`
folders and re-running `install` should fix many issues.