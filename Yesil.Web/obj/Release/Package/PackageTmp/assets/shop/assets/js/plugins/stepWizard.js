var StepWizard = function () {

    return {

        initStepWizard: function () {
            var form = $(".shopping-cart");
                form.validate({
                    errorPlacement: function errorPlacement(error, element) { element.before(error); },
                    rules: {
                        confirm: {
                            equalTo: "#password"
                        }
                    }
                });
        }, 

    };
}();        