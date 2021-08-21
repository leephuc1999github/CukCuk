<template>
  <div class="modal" v-if="open">
    <div class="modal-container">
      <div class="modal-header">
        <div class="box">
          <p class="title">Thông tin nhân viên</p>
          <div class="close-modal-site" @click="closeEmployeeDetail">
            <button class="close">&times;</button>
          </div>
        </div>
      </div>
      <div class="modal-content">
        <div class="row" style="padding: 24px">
          <div class="col-3">
            <div class="avatar-box">
              <div class="avatar-site"></div>
              <p style="text-align: center">
                (Vui lòng chọn ảnh có định dạng .jpg, .jpeg, .png, .gif)
              </p>
            </div>
          </div>
          <div class="col-9">
            <div class="info-box-general">
              <div
                class="row title-info-box"
                style="margin-top: 0 !important; padding-left: 16px"
              >
                <div class="tittle" style="font-size: 15px">
                  A. Thông tin chung :
                  <div class="progress-bottom"></div>
                </div>
              </div>
              <div class="row">
                <div class="col-6">
                  <div class="col-item">
                    <div class="label-input">
                      <p class="label">
                        Mã nhân viên(<span style="color: red">*</span>)
                      </p>
                      <input
                        type="text"
                        required
                        class="input"
                        name="EmployeeCode"
                        ref="code"
                        @blur="validateInputRequired('EmployeeCode', 'code')"
                        @focus="removeRedBorder('code')"
                        v-model="employee.EmployeeCode"
                      />
                      <div class="msg" ref="code-msg-required">
                        Mã nhân viên không được để trống
                      </div>
                      <div class="msg" ref="code-msg-duplicate">
                        Mã nhân viên đã tồn tại
                      </div>
                    </div>
                  </div>
                </div>
                <div class="col-6">
                  <div class="col-item">
                    <div class="label-input">
                      <p class="label">
                        Họ và tên(<span style="color: red">*</span>)
                      </p>
                      <input
                        type="text"
                        required
                        class="input"
                        name="FullName"
                        @blur="validateInputRequired('FullName', 'fullname')"
                        ref="fullname"
                        @focus="removeRedBorder('fullname')"
                        v-model="employee.FullName"
                      />
                      <div class="msg" ref="fullname-msg-required">
                        Họ và tên không được để trống
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div class="row">
                <div class="col-6">
                  <div class="col-item">
                    <div class="label-input">
                      <p class="label">Ngày sinh</p>
                      <date-picker
                        id="dob"
                        v-model="employee.DateOfBirth"
                        type="date"
                        :format="'DD/MM/YYYY'"
                        :value-type="'YYYY-MM-DD'"
                      ></date-picker>
                    </div>
                  </div>
                </div>
                <div class="col-6">
                  <div class="col-item">
                    <div class="label-input">
                      <p class="label">Giới tính</p>
                      <Dropdown
                        v-bind:type="employee.Type"
                        v-bind:value="employee.Gender"
                        v-bind:url="genderObj.Api"
                        v-bind:fields="genderObj.Fields"
                        v-bind:data="genderObj.Data"
                        v-bind:name="genderObj.Name"
                        v-bind:style="{ width: '100%' }"
                      ></Dropdown>
                    </div>
                  </div>
                </div>
              </div>
              <div class="row">
                <div class="col-6"></div>
                <div class="col-6"></div>
              </div>
              <div class="row">
                <div class="col-6">
                  <div class="col-item">
                    <div class="label-input">
                      <p class="label">
                        Số CMTND/ Căn cước(<span style="color: red">*</span>)
                      </p>
                      <input
                        type="text"
                        required
                        class="input"
                        name="IdentityNumber"
                        @blur="
                          validateInputRequired(
                            'IdentityNumber',
                            'identitynumber'
                          )
                        "
                        ref="identitynumber"
                        @focus="removeRedBorder('identitynumber')"
                        v-model="employee.IdentityNumber"
                      />
                      <div class="msg" ref="identitynumber-msg-required">
                        Số CMTND/ Căn cước không được để trống
                      </div>
                      <div class="msg" ref="identitynumber-msg-duplicate">
                        Số CMTND/ Căn cước không được trùng
                      </div>
                    </div>
                  </div>
                </div>
                <div class="col-6">
                  <div class="col-item">
                    <div class="label-input">
                      <p class="label">
                        Ngày cấp
                      </p>
                      <date-picker
                        id="identity-date"
                        v-model="employee.IdentityDate"
                        type="date"
                        :format="'DD/MM/YYYY'"
                        :value-type="'YYYY-MM-DD'"
                      ></date-picker>
                    </div>
                  </div>
                </div>
              </div>
              <div class="row">
                <div class="col-6">
                  <div class="col-item">
                    <div class="label-input">
                      <p class="label">Nơi cấp</p>
                      <input
                        type="text"
                        class="input"
                        name="IdentityPlace"
                        v-model="employee.IdentityPlace"
                      />
                    </div>
                  </div>
                </div>
                <div class="col-6">
                  <div class="col-item"></div>
                </div>
              </div>
              <div class="row">
                <div class="col-6">
                  <div class="col-item">
                    <div class="label-input">
                      <p class="label">
                        Email(<span style="color: red">*</span>)
                      </p>
                      <input
                        type="email"
                        pattern="/^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/"
                        required
                        class="input"
                        name="Email"
                        @blur="
                          validateInputRequired('Email', 'email');
                          validateInputFormat('Email', 'email');
                        "
                        ref="email"
                        @focus="removeRedBorder('email')"
                        v-model="employee.Email"
                      />
                      <div class="msg" ref="email-msg-required">
                        Email không được để trống
                      </div>
                      <div class="msg" ref="email-msg-format">
                        Email không đúng định dạng
                      </div>
                      <div class="msg" ref="email-msg-duplicate">
                        Email này đã được dùng
                      </div>
                    </div>
                  </div>
                </div>
                <div class="col-6">
                  <div class="col-item">
                    <div class="label-input">
                      <p class="label">
                        Số điện thoại(<span style="color: red">*</span>)
                      </p>
                      <input
                        type="text"
                        required
                        class="input"
                        name="PhoneNumber"
                        @blur="validateInputRequired('PhoneNumber', 'phone')"
                        ref="phone"
                        @focus="removeRedBorder('phone')"
                        v-model="employee.PhoneNumber"
                      />
                      <div class="msg" ref="phone-msg-required">
                        Số điện thoại không được để trống
                      </div>
                      <div class="msg" ref="phone-msg-duplicate">
                        Số điện thoại này đã được dùng
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="info-box-job">
              <div
                class="tittle title-info-box"
                style="font-size: 15px; padding-left: 16px"
              >
                B. Thông tin công việc :
                <div class="progress-bottom"></div>
              </div>
              <div class="row">
                <div class="row">
                  <div class="col-6">
                    <div class="col-item">
                      <div class="label-input">
                        <p class="label">Vị trí</p>
                        <Dropdown
                          v-bind:type="positionObj.Type"
                          v-bind:value="employee.PositionId"
                          v-bind:url="positionObj.Api"
                          v-bind:fields="positionObj.Fields"
                          v-bind:data="positionObj.Data"
                          v-bind:name="positionObj.Name"
                          v-bind:style="{ width: '100%' }"
                        ></Dropdown>
                      </div>
                    </div>
                  </div>
                  <div class="col-6">
                    <div class="col-item">
                      <div class="label-input">
                        <p class="label">Phòng ban</p>
                        <Dropdown
                          v-bind:type="workStatusObj.Type"
                          v-bind:value="employee.DepartmentId"
                          v-bind:url="departmentObj.Api"
                          v-bind:fields="departmentObj.Fields"
                          v-bind:data="departmentObj.Data"
                          v-bind:name="departmentObj.Name"
                          v-bind:style="{ width: '100%' }"
                        ></Dropdown>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="col-6">
                    <div class="col-item">
                      <div class="label-input">
                        <p class="label">Mã số thuế</p>
                        <input type="text" class="input" />
                      </div>
                    </div>
                  </div>
                  <div class="col-6">
                    <div class="col-item">
                      <div class="label-input">
                        <p class="label">Mức lương cơ bản</p>
                        <div style="position: relative; display: flex">
                          <money
                            id="salary"
                            v-model="employee.Salary"
                            v-bind="money"
                          ></money>
                          <div class="price-txt">(VNĐ)</div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="col-6">
                    <div class="col-item">
                      <div class="label-input">
                        <p class="label">Ngày gia nhập công ty</p>
                        <date-picker
                          id="join-date"
                          v-model="employee.JoinDate"
                          type="date"
                          :format="'DD/MM/YYYY'"
                          :value-type="'YYYY-MM-DD'"
                        ></date-picker>
                      </div>
                    </div>
                  </div>
                  <div class="col-6">
                    <div class="col-item">
                      <div class="label-input">
                        <p class="label">Tình trạng công việc</p>
                        <Dropdown
                          v-bind:type="workStatusObj.Type"
                          v-bind:value="employee.WorkStatus"
                          v-bind:url="workStatusObj.Api"
                          v-bind:fields="workStatusObj.Fields"
                          v-bind:data="workStatusObj.Data"
                          v-bind:name="workStatusObj.Name"
                          v-bind:style="{ width: '100%' }"
                        ></Dropdown>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="modal-footer">
        <div class="modal-action">
          <div class="action-box">
            <button class="btn-default cancle" @click="closeEmployeeDetail">
              Hủy
            </button>
            <button class="btn-icon" id="btnSave" @click="saveForm">
              <i class="fa fa-save"></i>
              Lưu
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { Money } from "v-money";
import Dropdown from "../../components/base/BaseDropdown.vue";
import EventBus from "../../EventBus";
import DatePicker from "vue2-datepicker";
import axios from "axios";
import Common from "../../Common";
export default {
  name: "EmployeeDetail",
  components: {
    Dropdown,
    Money,
    DatePicker,
  },
  data() {
    return {
      styleValidate: {},

      money: {
        decimal: ",",
        thousands: ".",
        precision: 0,
        masked: false,
      },

      open: false,
      method: null,
      id: null,
      resultAction: {},
      tabInputRequired: false,

      employee: {},
      // properties for field position
      positionObj: {
        Type: "input",
        Name: "PositionId",
        Data: [],
        Fields: ["PositionId", "PositionName"],
        Api: "http://cukcuk.manhnv.net/v1/Positions",
      },
      // properties for field department
      departmentObj: {
        Type: "input",
        Name: "DepartmentId",
        Data: [],
        Fields: ["DepartmentId", "DepartmentName"],
        Api: "http://cukcuk.manhnv.net/api/Department",
      },
      // gender options
      genderObj: {
        Type: "input",
        Name: "Gender",
        Data: [
          { Text: "Nam", Value: 1 },
          { Text: "Nữ", Value: 0 },
          { Text: "Không xác định", Value: 2 },
        ],
        Fields: [],
        Api: "",
      },

      // workstatus
      workStatusObj: {
        Type: "input",
        Name: "WorkStatus",
        Data: [
          { Text: "Đang làm việc", Value: "" },
          { Text: "Đã nghỉ việc", Value: "" },
        ],
        Fields: [],
        Api: "",
      },

      flag: false,
    };
  },
  created() {
    // catch open employee detail
    EventBus.$on("openEmployeeDetail", (value) => {
      this.open = value;
      this.$nextTick(function () {
        this.$refs.code.focus();
      });
    });

    /**
     * catch information form
     * Author : LP(7/8)
     */
    EventBus.$on("form", (value) => {
      this.id = value.Id;
      this.method = value.Method;
    });

    // catch event dropdown changed
    EventBus.$on("changed", (selected) => {
      switch (selected.Name) {
        case "PositionId":
          this.employee.PositionId = selected.Value;
          break;
        case "DepartmentId":
          this.employee.DepartmentId = selected.Value;
          break;
        case "Gender":
          this.employee.Gender = selected.Value;
          break;
      }
    });
  },
  mounted() {},
  watch: {
    async method() {
      if (this.method === "POST") {
        await this.getNewEmployeeCode();
      } else {
        if (!Common.isNullOrUndifined(this.id)) {
          await this.getEmployee();
          this.validateInputRequired("EmployeeCode", "code");
          this.validateInputRequired("FullName", "fullname");
          this.validateInputRequired("Email", "email");
          this.validateInputRequired("PhoneNumber", "phone");
          this.validateInputRequired("IdentityNumber", "identitynumber");
        }
      }
    },
  },
  methods: {
    /**
     *
     */
    async saveForm() {
      this.flag = false;
      if (
        Common.isNullOrUndifined(this.employee.EmployeeCode) ||
        Common.isNullOrUndifined(this.employee.FullName) ||
        Common.isNullOrUndifined(this.employee.Email) ||
        Common.isNullOrUndifined(this.employee.PhoneNumber) ||
        Common.isNullOrUndifined(this.employee.IdentityNumber)
      ) {
        this.validateInputRequired("EmployeeCode", "code");
        this.validateInputRequired("FullName", "fullname");
        this.validateInputRequired("Email", "email");
        this.validateInputRequired("PhoneNumber", "phone");
        this.validateInputRequired("IdentityNumber", "identitynumber");
        this.$toast.info("Bạn cần nhập đẩy đủ thông tin yêu cầu");
      } else {
        if (this.method === "POST") {
          let r1 = await this.duplicated("Email", this.employee.Email);
          let r2 = await this.duplicated(
            "PhoneNumber",
            this.employee.PhoneNumber
          );
          let r3 = await this.duplicated(
            "EmployeeCode",
            this.employee.EmployeeCode
          );
          let r4 = await this.duplicated(
            "IdentityNumber",
            this.employee.IdentityNumber,
            "identitynumber"
          );
          if (!Common.isNullOrUndifined(r1.data.Data)) {
            this.validateInputDuplicate("email");
            this.flag = true;
          }
          if (!Common.isNullOrUndifined(r2.data.Data)) {
            this.validateInputDuplicate("phone");
            this.flag = true;
          }
          if (!Common.isNullOrUndifined(r3.data.Data)) {
            this.validateInputDuplicate("code");
            this.flag = true;
          }
          if (!Common.isNullOrUndifined(r4.data.Data)) {
            this.validateInputDuplicate("identitynumber");
            this.flag = true;
          }
        }
        if (!this.flag) {
          switch (this.method) {
            case "POST":
              this.addEmployee();
              break;

            default:
              this.editEmpoyee();
              break;
          }
          this.closeEmployeeDetail();
        }
      }
    },

    /**
     * validate input required
     * Author : LP(6/8)
     */
    validateInputRequired(value, ref) {
      if (Common.isNullOrUndifined(this.employee[value])) {
        if (value != "IdentityDate") {
          this.$refs[ref].classList.value += " red-border";
          this.employee[value] = "";
        }
        this.$refs[ref + "-msg-required"].style.display = "block";
      }
    },

    /**
     * validate input foramt
     * Author : LP(11/8)
     */
    validateInputFormat(value, ref) {
      if (
        !Common.validateEmail(this.employee[value]) &&
        !Common.isNullOrUndifined(this.employee[value])
      ) {
        this.$refs[ref + "-msg-format"].style.display = "block";
      }
    },

    /**
     * validate input duplicate
     * Author : LP(12/8)
     */
    validateInputDuplicate(ref) {
      this.$refs[ref + "-msg-duplicate"].style.display = "block";
      this.$refs[ref].classList.value += " red-border";
    },

    /**
     *
     */
    async duplicated(name, value) {
      try {
        let response = await axios.get(
          Common.APIURL + `employees/Property?name=${name}&value=${value}`
        );
        return response;
      } catch (error) {
        console.log("getResultDuplicate\n" + error);
      }
    },

    /**
     * remove red border
     * Author : LP(6/8)
     */
    removeRedBorder(value) {
      let clInput = this.$refs[value].classList.value;
      this.$refs[value].classList.value = clInput.replaceAll("red-border", "");
      this.$refs[value + "-msg-required"].style.display = "none";
      if (value === "email")
        this.$refs[value + "-msg-format"].style.display = "none";
      if (
        value === "code" ||
        value === "email" ||
        value === "identitynumber" ||
        value === "phone"
      )
        this.$refs[value + "-msg-duplicate"].style.display = "none";
      // debugger; // eslint-disable-line
      // this.tabInputRequired = false;
    },

    /**
     * put a employee
     * Author : LP(8/8)
     */
    async editEmpoyee() {
      try {
        let response = await axios.put(
          Common.APIURL + `employees/${this.id}`,
          this.employee
        );
        EventBus.$emit("resultForm", response);
      } catch (error) {
        console.log("editEmployee\n" + error);
      }
    },

    /**
     * post a employee
     * Author : LP(8/8)
     */
    async addEmployee() {
      try {
        let response = await axios.post(
          Common.APIURL + "employees",
          this.employee
        );
        EventBus.$emit("resultForm", response);
      } catch (error) {
        console.log("addEMployee\n" + error);
      }
    },

    /**
     * get new employee code
     * Author : LP(7/8)
     */
    async getNewEmployeeCode() {
      try {
        let response = await axios.get(
          Common.APIURL + "employees/NewEmployeeCode"
        );
        this.employee = {
          EmployeeCode: "",
          DepartmentId: "",
          PositionId: "",
          Gender: 1,
        };
        this.employee.EmployeeCode = response.data.Data;
      } catch (error) {
        console.log("getNewEmployeeCode\n" + error);
      }
    },

    /**
     * get employee by id
     * Author : LP(7/8)
     */
    async getEmployee() {
      try {
        let response = await axios.get(
          Common.APIURL + `employees/${this.id}`
        );
        this.employee = {};
        this.employee = response.data.Data;
        if (Common.isNullOrUndifined(this.employee.Salary))
          this.employee.Salary = 0;
      } catch (error) {
        console.log("getEmployee\n" + error);
      }
    },

    /**
     * close form employee detail
     * Author : LP(6/8)
     */
    closeEmployeeDetail() {
      this.open = false;
      this.employee = {};
      this.method = null;
      this.id = null;
      this.flag = false;
    },
  },
  beforeDestroy() {
    EventBus.$off("openEmployeeDetail");
    EventBus.$off("form");
    EventBus.$off("changed");
  },
};
</script>

<style scoped>
.msg {
  font-family: "GoogleSans-Italic";
  color: #f65454;
  margin-top: 5px;
  display: none;
  font-size: 12px;
}

#dob,
#join-date,
#identity-date {
  width: 100% !important;
}
#dob .mx-input-wrapper {
  height: 40px;
}
#salary {
  width: 100%;
  height: 38px;
  outline: none;
  border: 1px solid #bbbbbb;
  border-radius: 4px;
  padding: 0;
  margin: 0;
  text-align: right;
  padding-right: 56px;
  font-family: "GoogleSans-Regular";
}
#salary:focus {
  border: 1px solid #01b075;
}
.title {
  font-size: 20px;
  font-family: "GoogleSans-Bold";
  margin: 0;
  line-height: 40px;
}
.btn-default {
  cursor: pointer;
  border: 1px solid #bbbbbb;
  padding: 0 24px;
  text-align: center;
  background-color: #ffffff;
  border-radius: 4px;
  height: 40px;
  min-width: 100px;
  font-size: 13px;
}

.btn-default:hover {
  border: none;
  background-color: #e9ebee;
}

.btn-default:focus {
  /* background-color: #01b075; */
  border: none;
  color: #bbbbbb;
}

/* core btn */
.btn-icon {
  height: 40px;
  background-color: #019160;
  outline: none;
  color: #ffffff;
  border: none;
  border-radius: 4px;
  font-size: 13px;
  padding: 0 16px;
  min-width: 100px;
  font-family: "GoogleSans-Regular";
}
.btn-icon > i {
  padding-right: 8px;
}

.btn-icon:hover {
  background-color: #2fbe8e;
}
.btn-icon:focus {
  background-color: #01b075;
}
/* core modal */
.modal {
  position: fixed;
  z-index: 1;
  left: 0;
  top: 0;
  width: 100%;
  height: 100%;
  overflow: auto;
  background-color: rgb(0, 0, 0);
  background-color: rgba(0, 0, 0, 0.4);
}

.modal-container {
  background-color: #fefefe;
  margin: 0 auto;
  width: 700px;
  position: relative;
  display: block;
  height: calc(100vh - 20px);
  margin-top: 10px;
  margin-bottom: 10px;
  padding: 0 !important;
  border-radius: 4px;
}
.modal-header {
  position: absolute;
  top: 0;
  background-color: #fefefe;
  display: block;
  width: 100%;
  border-radius: 4px;
  /* background-color: red; */
}
.modal-header .box {
  padding: 24px;
}
.close-modal-site {
  height: 38px;
  width: 38px;
  position: absolute;
  right: 0;
  top: 0;
  border: 1px solid #e5e5e5;
  border-radius: 4px;
  background-color: #e5e5e5;
  text-align: center;
}
.close {
  background: none;
  border: none;
  text-align: center;
  font-size: 30px;
  color: #ccc;
  cursor: pointer;
}
.modal-content {
  height: calc(100% - 64.8px - 60px);
  overflow-y: scroll;
  position: absolute;
  top: 64.8px;
}

.modal-footer {
  width: 100%;
  height: 60px;
  display: flex;
  background: #e5e5e5;
  bottom: 0;
  position: absolute;
  border-radius: 4px;
}

.modal-action {
  align-self: center;
  margin-left: auto;
  padding: 0 24px;
}

.action-box > button {
  margin-left: 16px;
}

/* end core modal */

/* core search box */
.search-box {
  border: 1px solid #bbbbbb;
  border-radius: 4px;
  width: 300px;
}
.search-box:hover {
  border: 1px solid #01b075;
}
.box {
  display: flex;
}
.search-icon-site {
  width: 16px;
  height: 16px;
  align-self: center;
  margin: 0 8px 0 16px;
}
.search-icon-site > i {
  font-size: 16px;
  color: #bbbbbb;
}
.input-search {
  border: none;
  outline: none;
  width: 100%;
  height: 38px;
  font-family: "GoogleSans-Regular";
  font-size: 13px;
  padding-right: 16px;
}
.reset-txt-icon {
  font-size: 16px;
  padding: 0 10px;
  display: flex;
  color: #bbbbbb;
}
.reset-txt-icon > i {
  display: none;
  align-self: center;
  cursor: pointer;
}
/* end core search box */

/* core input label */
.label {
  padding: 0;
  margin: 0;
  padding-bottom: 4px;
  font-size: 13px;
}

.input {
  outline: none;
  border: 1px solid;
  border-color: #bbbbbb;
  height: 40px;
  border-radius: 4px;
  box-sizing: border-box;
  padding-left: 16px;
  padding-right: 16px;
  font-size: 12px;
  font-family: "GoogleSans-Regular";
}

.input:focus {
  border: 1px solid #01b075;
}

/* end core input label */

.avatar-box {
  width: 100%;
}

.avatar-site {
  background: url("../../assets/img/default-avatar.jpg");
  background-repeat: no-repeat;
  background-size: contain;
  width: 150px;
  height: 150px;

  background-size: contain;
  border-radius: 50%;
  background-color: #ffffff;
  border: 1px solid #e5e5e5;
  margin: 0 auto;
}

.info-box-job .row,
.info-box-general .row {
  padding: 8px 0;
}

.col-item {
  width: calc(100% - 16px);
  float: right !important;
}
.label-input .input {
  width: 100%;
}
#viewDetail {
  display: none;
}

.progress-bottom {
  height: 5px;
  background-color: #019160;
  width: 15%;
  margin-top: 8px;
}

.title-info-box {
  margin: 24px 0;
}

.price-txt {
  position: absolute;
  right: 0;
  align-self: center;
  padding-right: 16px;
}

.red-border {
  border: 1px solid red !important;
}
</style>