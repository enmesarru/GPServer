import Schema from "async-validator";

export default {
    data() {
        return {
            isFail: false,
            errorMessages: {},
        };
    },
    created() {
        if(!this.rules && !this.formData) {
            return;
        }
        this.schema = new Schema(this.rules);
    },
    methods: {
        validateForm() {
            this.errorMessages = {};
            this.isFail = false;
            
            let validationState = true;

            this.schema.validate(this.formData, (errors) => {
                if(errors) {
                    validationState = false;
                    this.isFail = true;

                    errors.forEach(({field, message}) => {
                        this.errorMessages[field] = message.replace(field, 'Field');
                    });
                }   
            });
            return validationState;
        }
    }
};