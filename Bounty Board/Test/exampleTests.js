//Each level of nesting is concatenated to form the unit test's full name
describe("Example", function () {

    describe("- Basic:", function () {
        //"Example - Basic: test should pass"
        it("test should pass", function () {
            expect(true).toBe(true);
        });

        //"Example - Basic: test should fail"
        it("test should fail", function () {
            expect(false).toBe(true);
        });
    });

    describe("- Angular Injection:", function () {


        it("'test' component is defined (this will fail because it does not exist)", function () {
            var test;

            //specify we are currently using the 'app' module within Angular
            module('app');

            //Call to Angular to Inject an Angular component
            //  ----------------------
            //"_test_" is a reference to the angular component "test"
            //  This _ syntax is an angular feature that is used because
            //  most of the time you will be assigning the requested component
            //  to a variable of the same name.
            inject(function (_test_) {
                test = _test_;
            });

            expect(test).not.toBeDefined();
        });
    });
});