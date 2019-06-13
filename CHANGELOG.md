## [2.0.1](https://github.com/ashblue/fluid-state-machine/compare/v2.0.0...v2.0.1) (2019-06-13)


### Bug Fixes

* **fsm:** state not initially set on build ([8e7e67a](https://github.com/ashblue/fluid-state-machine/commit/8e7e67a))

# [2.0.0](https://github.com/ashblue/fluid-state-machine/compare/v1.0.0...v2.0.0) (2019-06-02)


### Code Refactoring

* **namespaces:** changed to match fluid behavior tree ([6afdbe8](https://github.com/ashblue/fluid-state-machine/commit/6afdbe8))


### Features

* **builders:** removed class inheritance in favor of c# extensions ([be1618a](https://github.com/ashblue/fluid-state-machine/commit/be1618a))
* **git:** added commitizen support ([db87261](https://github.com/ashblue/fluid-state-machine/commit/db87261))


### BREAKING CHANGES

* **namespaces:** Namespace must be converted from CleverCrow.FluidStateMachine to
CleverCrow.Fluid.FSMs with a simple find a replace across your scripts
* **builders:** Current custom builders must be replaced with C# extensions that target the builder
directly. See README.md section on extension for an example.
