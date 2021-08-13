<template>
  <div class="employee-list">
    <div class="content-header">
      <div class="heading">
        <p class="title">Danh sách nhân viên</p>
        <div class="add-employee-site">
          <button class="btn-icon" @click="toAddEmployee">
            <i class="fa fa-user-plus"></i>
            Thêm nhân viên
          </button>
        </div>
      </div>
      <div class="filter">
        <div class="filter-item" style="margin-left: 0">
          <SearchBox></SearchBox>
        </div>
        <div class="filter-item">
          <Dropdown
            v-bind:type="positionObj.Type"
            v-bind:url="positionObj.Api"
            v-bind:fields="positionObj.Fields"
            v-bind:data="positionObj.Data"
            v-bind:name="positionObj.Name"
          ></Dropdown>
        </div>
        <div class="filter-item">
          <Dropdown
            v-bind:type="departmentObj.Type"
            v-bind:url="departmentObj.Api"
            v-bind:fields="departmentObj.Fields"
            v-bind:data="departmentObj.Data"
            v-bind:name="departmentObj.Name"
          ></Dropdown>
        </div>

        <button class="reload-site" @click="refreshTable()">
          <div class="reload-icon"></div>
        </button>
      </div>
    </div>
    <div class="content-body">
      <table id="datatable">
        <thead>
          <tr>
            <td>#</td>
            <th>Mã nhân viên</th>
            <th>Họ và tên</th>
            <th>Giới tính</th>
            <th>Ngày sinh</th>
            <th>Diện thoại</th>
            <th>Email</th>
            <th>Chức vụ</th>
            <th>Phòng ban</th>
            <th class="txt-right">Mức lương cơ bản</th>
            <th>Tình trạng công việc</th>
            <th>Thao tác</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(employee, index) in employees" :key="employee.EmployeeId">
            <td>{{ index + 1 }}</td>
            <td>{{ employee.EmployeeCode }}</td>
            <td>{{ employee.FullName }}</td>
            <td>{{ employee.Gender | formatGender }}</td>
            <td>{{ employee.DateOfBirth | formatDOB }}</td>
            <td>{{ employee.PhoneNumber }}</td>
            <td v-bind:title="employee.Email">{{ employee.Email }}</td>
            <td>{{ employee.PositionName }}</td>
            <td>{{ employee.DepartmentName }}</td>
            <td class="txt-right">{{ employee.Salary | formatMoney }}</td>
            <td>{{ employee.WorkStatus }}</td>
            <td>
              <button
                class="misa-btn misa-btn-edit"
                @click="toEditEmployee(employee.EmployeeId)"
              >
                <i class="fa fa-edit"></i>
              </button>

              <button
                class="misa-btn misa-btn-delete"
                @click="
                  toDeleteEmployee(employee.EmployeeCode, employee.EmployeeId)
                "
              >
                <i class="fa fa-trash"></i>
              </button>
            </td>
          </tr>
          <tr v-if="employees.length == 0 ? true : false">
            <td colspan="12" style="text-align: center">
              Hiện không có dữ liệu nào !
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="content-footer">
      <Pagination v-bind:active="pageIndex" v-bind:size="pageSize"></Pagination>
    </div>
    <EmployeeDetail></EmployeeDetail>
    <Toast
      v-bind:name="toastObj.Name"
      v-bind:type="toastObj.Type"
      v-bind:content="toastObj.Content"
    >
    </Toast>

    <Popup
      v-bind:header="popupObj.Header"
      v-bind:content="popupObj.Content"
    ></Popup>
  </div>
</template>

<script>
import EmployeeDetail from "./EmployeeDetail.vue";
import SearchBox from "../../components/base/BaseSearchBox.vue";
import Dropdown from "../../components/base/BaseDropdown.vue";
import EventBus from "../../EventBus";
import Common from "../../Common";
import axios from "axios";
import Toast from "../../components/base/BaseToast.vue";
import Popup from "../../components/base/BasePopup.vue";
import Pagination from "../../components/base/BasePagination.vue";
export default {
  name: "Employee",
  components: {
    Toast,
    Popup,
    SearchBox,
    Dropdown,
    EmployeeDetail,
    Pagination,
  },
  data() {
    return {
      headers: [
        "Mã nhân viên",
        "Họ và tên",
        "Giới tính",
        "Ngày sinh",
        "Điện thoại",
        "Email",
        "Chức vụ",
        "Phòng ban",
        "Mức lương cơ bản",
        "Tình trạng công việc",
        "Thao tác",
      ],

      toastObj: {
        Name: "success",
        Type: "success",
        Content: "Thêm mới thành công",
      },

      popupObj: {
        Header: "Xóa nhân viên",
        Content: "Bạn có muốn xóa nhân viên không ?",
      },

      open: false,
      employeeId: "",
      employees: [],

      //properties for position dropdown
      positionObj: {
        Type: "filter",
        Name: "position",
        Data: [{ Text: "Tất cả vị trí", Value: "" }],
        Fields: ["PositionId", "PositionName"],
        Api: "http://cukcuk.manhnv.net/v1/Positions",
      },
      //properties for department dropdown
      departmentObj: {
        Type: "filter",
        Name: "department",
        Data: [{ Text: "Tất cả phòng ban", Value: "" }],
        Fields: ["DepartmentId", "DepartmentName"],
        Api: "http://cukcuk.manhnv.net/api/Department",
      },

      keyword: "",
      positionId: "",
      departmentId: "",
      pageIndex: 1,
      pageSize: 12,
    };
  },
  created() {
    // get all employees
    this.filterEmployees(
      this.pageIndex,
      this.pageSize,
      this.keyword,
      this.departmentId,
      this.positionId
    );

    // catch event searh box
    EventBus.$on("keyChange", (keyword) => {
      this.keyword = keyword;
      this.filterEmployees(
        this.pageIndex,
        this.pageSize,
        this.keyword,
        this.departmentId,
        this.positionId
      );
    });

    // catch event dropdown changed
    EventBus.$on("changed", (selected) => {
      switch (selected.Name) {
        case "position":
          this.positionId = selected.Value;
          break;
        case "department":
          this.departmentId = selected.Value;
          break;
      }
      if (selected.Type === "filter")
        this.filterEmployees(
          this.pageIndex,
          this.pageSize,
          this.keyword,
          this.departmentId,
          this.positionId
        );
    });

    // catch event result in form
    EventBus.$on("resultForm", (result) => {
      this.logging(result);
      // EventBus.$emit("openToast", true);
      this.filterEmployees(
        this.pageIndex,
        this.pageSize,
        this.keyword,
        this.departmentId,
        this.positionId
      );
    });

    // catch event confirmed delete
    EventBus.$on("confirmed", (value) => {
      if (value) {
        this.deleteEmployee();
      }
    });

    EventBus.$on("pagination", (page) => {
      this.pageIndex = page;
      this.filterEmployees(
        this.pageIndex,
        this.pageSize,
        this.keyword,
        this.departmentId,
        this.positionId
      );
    });
  },
  mounted() {},
  computed: {},
  watch: {},
  filters: {
    formatDOB: function (value) {
      return Common.formatDateDDMMYYYY(value);
    },

    formatMoney: function (value) {
      return Common.formatMoneyToVND(value);
    },

    formatGender: function (value) {
      switch (value) {
        case 0:
          return "Nữ";
        case 1:
          return "Nam";
        case 2:
          return "Không xác định";
        default:
          return "";
      }
    },
  },
  methods: {
    /**
     * Delete a employee
     * Author : LP(8/8)
     */
    async deleteEmployee() {
      try {
        let response = await axios.delete(
          Common.APIURL + `employees/${this.employeeId}`
        );
        EventBus.$emit("resultForm", response);
      } catch (error) {
        console.log("deleteEmployee\n" + error);
      }
    },

    /**
     * Logging response
     * Author : LP(8/8)
     */
    logging(response) {
      console.log(response);
      switch (response.data.ResultCode) {
        case 200:
          this.$toast.success("Thực hiện thành công");
          break;
        case 500:
          this.$toast.error("Thực hiện không thành công");
          break;
        default:
          this.$toast.success("Thực hiện thành công");
          break;
      }
    },

    /**
     * Todo delete
     * Author : LP(8/8)
     */
    toDeleteEmployee(code, id) {
      this.popupObj.Content = `Bạn có thực sự muốn xóa nhân viên ${code} không ?`;
      EventBus.$emit("openPopup", true);
      this.employeeId = id;
    },

    /**
     * Todo modal
     * Author : LP(7/8)
     */
    toAddEmployee() {
      EventBus.$emit("openEmployeeDetail", true);
      EventBus.$emit("form", { Method: "POST", Id: "" });
    },

    /**
     * Open Employee Detail \
     * Author : LP(7/8)
     */
    toEditEmployee(id) {
      EventBus.$emit("openEmployeeDetail", true);
      EventBus.$emit("form", { Method: "PUT", Id: id });
    },

    /**
     * Filter data by keyword, departmentId, positionId api
     * Author : LP(7/8)
     */
    async filterEmployees(
      pageIndex,
      pageSize,
      keyword,
      departmentId,
      positionId
    ) {
      try {
        let result = await axios.get(
          Common.APIURL +
            `employees/paging?keyword=${keyword}&positionId=${positionId}&departmentId=${departmentId}&pageIndex=${
              (pageIndex-1) * pageSize
            }&pageSize=${pageSize}`
        );
        this.employees = result.data;
      } catch (error) {
        console.log("filterEmployees\n" + error);
      }
    },
  },
  beforeDestroy() {},
};
</script>

<style scoped>
.misa-btn {
  background: none;
  border: none;
  cursor: pointer;
}
.misa-btn-delete:hover {
  color: #f65454;
}
.misa-btn-edit:hover {
  color: #01b075;
}
.txt-right {
  text-align: right;
}
.content-body {
  overflow-x: scroll;
  display: block;
  height: 590px;
}
table {
  width: 100%;
  border-collapse: collapse;
}
tr {
  height: 48px;
}
td,
th {
  white-space: nowrap;
  padding: 0 8px 0 10px;
  text-align: left;
}

tbody tr:nth-child(odd) {
  background: #e5e5e5;
}
thead {
  position: sticky;
  top: 0;
  background: #fff;
}
td {
  max-width: 100px;
  text-overflow: ellipsis;
  overflow: hidden;
}
.employee-list {
  margin: 0 16px;
  /* display: none; */
  height: 100%;
}

.title {
  font-size: 20px;
  font-family: "GoogleSans-Bold";
  margin: 0;
  line-height: 40px;
}
.content-header {
  width: 100%;
  margin-bottom: 16px;
}

.heading {
  display: flex;
  padding-top: 24px;
  white-space: nowrap;
}
.filter {
  display: flex;
  padding-top: 16px;
}
.heading > .add-employee-site {
  margin-left: auto;
  align-self: center;
}

.filter > .filter-item {
  margin: 0 8px;
}

.reload-site {
  cursor: pointer;
  align-self: center;
  margin-left: auto;
  border: 1px solid #bbbbbb;
  border-radius: 5px;
  height: 40px;
  width: 40px;
  background-color: #ffffff;
  display: flex;
}
.reload-site:focus {
  border: 1px solid #01b075;
  outline: none;
}

.reload-site > .reload-icon {
  background: url("../../assets/icon/refresh.png");
  background-repeat: no-repeat;
  background-size: contain;
  background-position: center;
  height: 20px;
  width: 100%;
  margin: auto 0;
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
/* end core btn */

@media screen and (max-height: 768px) {
  .content-body {
    height: 520px;
  }
}
</style>