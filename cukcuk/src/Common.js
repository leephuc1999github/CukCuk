import moment from 'moment'
class Common {

    /**-------------------------------------------
    * Format salary to money VND
    * Author : LP(30/7)
    */
    static formatMoneyToVND(salary) {
        try {
            if (typeof(salary) != "number") return "";
            let result = salary.toLocaleString("vi", {
                style: "currency",
                currency: "VND",
            });
            return result.substring(0, result.length - 2);
        } catch (error) {
            console.log("formatMoneyToVND EmployeeList\n" + error);
        }
    }


    /**------------------------------------------
     * Format data of birth
     * Author : LP(31/7)
     */
    static formatDateDDMMYYYY(date) {
        if(Common.isNullOrUndifined(date)) return "";
        try {
            return moment(date).format("DD/MM/yyyy");
        } catch (error) {
            console.log("formatDateDDMMYYYY\n" + error);
        }
    }

    /**---------------------------------------------------
    * Value is null or undifined
    * Author :  LP(30/7)
    */
    static isNullOrUndifined(value) {
        return typeof(value) === "undefined" || value === null || value === ""
            ? true
            : false;
    }

    /**--------------------------------------------------
     * get list class in element
     * Author : LP(7/8)
     */
    static listClassElement(value){
        return value.split(" ");
    }

    /**
     * Validate Email
     * @param {*} email 
     * @returns 
     * Author : LP(8/11)
     */
     static validateEmail(email) {
        const patternEmail = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return patternEmail.test(email);
    }
}

export default Common;