<template>
  <div class="content">
    <div class="content__header">
      <div class="content__header__left">
        <div class="text text--heading">Danh sách nhân viên</div>
      </div>
      <div class="content__header__right">
        <base-button
          :className="
            `btn--danger padding-left--16 padding-right--16 margin-right--10 ${
              employeeIdList.length === 0 ? 'btn--hidden' : ''
            }`
          "
          @onclick="removeEmployee"
        >
          <div class="text text--color-white text--center">
            Xóa nhân viên
          </div>
        </base-button>
        <base-button
          className="padding-left--16 padding-right--16"
          @onclick="showForm"
        >
          <div class="icon icon--13 icon--add" style="margin-right:8px"></div>
          <div class="text text--color-white text--center">
            Thêm nhân viên
          </div>
        </base-button>
      </div>
    </div>
    <div class="content__toolbar">
      <div class="content__toolbar__left">
        <base-input
          icon="icon--search"
          placeholder="Tìm kiếm theo Mã, Tên hoặc Số điện thoại"
        ></base-input>
        <combo-box
          :data="department"
          :value="
            departmentSearch !== null
              ? departmentSearch
              : search['departmentId']
          "
          @onchangeinput="searchTextComboxbox"
          id="department"
        >
          <template v-slot:combo-box-options>
            <combo-box-option
              v-for="item in department"
              :key="item.id"
              :value="item.id"
              :label="item.label"
              :checked="item.id === search.departmentId"
              @select-item="selectItem"
            ></combo-box-option>
          </template>
        </combo-box>
        <combo-box
          :value="search['positionId']"
          :data="position"
          @onchangeinput="searchTextComboxbox"
          id="position"
        >
          <template v-slot:combo-box-options>
            <combo-box-option
              v-for="item in position"
              :key="item.id"
              :value="item.id"
              :label="item.label"
              :checked="item.id === search['positionId']"
              @select-item="selectItem"
            ></combo-box-option>
          </template>
        </combo-box>
      </div>
      <div class="content__toolbar__right">
        <base-button
          className="btn--secondary padding-left--10 padding-right--10"
          @onclick="loadData"
        >
          <div class="icon icon--24 icon--refresh"></div>
        </base-button>
      </div>
    </div>
    <div class="content__body">
      <div class="content__body__grid">
        <base-table>
          <template v-slot:thead>
            <base-table-head :columns="columns"></base-table-head>
          </template>
          <template v-slot:tbody>
            <base-table-body
              :columns="columns"
              :data="employeeList"
              @check-box="checkbox"
            ></base-table-body>
          </template>
        </base-table>
      </div>
    </div>
    <div class="content__footer">
      <base-pagination></base-pagination>
    </div>
    <base-loader :is-loading="true"></base-loader>
    <EmployeeDetail
      :is-showed="isShowed"
      :form-mode="formMode"
      :department="department"
      :position="position"
      :employee-id="employeeId"
      @show-form="showForm"
    />
    <base-toast-message
      :type="toast.type"
      :message="toast.message"
      :isShowed="toast.isShowed"
      @close="showToast"
    ></base-toast-message>
  </div>
</template>

<script>
import EmployeesAPI from "@/api/components/EmployeesAPI.js";
import PositionAPI from "@/api/components/PositionAPI.js";
import DepartmentAPI from "@/api/components/DepartmentAPI.js";
import { columns } from "@/views/employee/Column.js";
import EmployeeDetail from "@/views/employee/EmployeeDetail";
import _ from "lodash";

export default {
  components: { EmployeeDetail },

  data() {
    return {
      columns: columns,
      employeeList: [],
      employeeIdList: [],
      department: [],
      position: [],
      departmentForm: [],
      positionForm: [],
      search: {
        departmentId: "1",
        positionId: "1",
        textSearch: "",
      },
      positionSearch: null,
      departmentSearch: null,
      employeeId: null,
      isLoading: false,
      isShowed: false,
      formMode: 1,
      toast: {
        type: "done",
        message: "Tải dữ liệu thành công",
        isShowed: false,
      },
    };
  },
  created() {
    // /*
    //     Lấy dữ liệu các vị trí từ api
    // */
    // PositionAPI.getAll().then((res) => {
    //   let tmp = res.data.map(({ PositionId, PositionName }) => ({
    //     id: PositionId,
    //     label: PositionName,
    //     checked: false,
    //   }));

    //   tmp.push({ id: "1", label: "Tất cả vị trí", checked: true });
    //   this.position = tmp;
    // });

    // /*
    //     Lấy dữ liệu các phòng ban từ api
    // */
    // DepartmentAPI.getAll().then((res) => {
    //   let tmp = res.data.map(({ DepartmentId, DepartmentName }) => ({
    //     id: DepartmentId,
    //     label: DepartmentName,
    //     checked: false,
    //   }));

    //   tmp.push({ id: "1", label: "Tất cả phòng ban", checked: true });
    //   this.department = tmp;
    // });

    // /*
    //     Lấy dữ liệu nhân viên từ api
    // */
    // this.loadData();
    this.isLoading = true;
    Promise.all([
      PositionAPI.getAll(),
      DepartmentAPI.getAll(),
      EmployeesAPI.getAll(),
    ])
      .then((res) => {
        console.log(res);
      })
      .catch(() => {
        this.toast.type = "danger";
        this.toast.message = "Tải dữ liệu thất bại!";
        this.toast.isShowed = true;
      });
  },

  watch: {
    isLoading(newVal) {
      if (newVal === true) {
        this.departmentForm = _.cloneDeep(this.department);
        this.positionForm = _.cloneDeep(this.position);
      }
    },
  },

  computed: {},

  methods: {
    /*
        Lấy dữ liệu nhân viên từ api
    */
    loadData() {
      EmployeesAPI.getAll().then((res) => {
        this.employeeList = res.data;
      });
    },

    /*
      Xử lý chọn option trong cac combobox
    */
    selectItem(item) {
      this.search[item.key] = item.id;
    },

    /*
      Tìm kiếm theo text trong combobox
    */
    searchTextComboxbox({ value, key }) {
      let text = _.trim(value);

      if (text === "" || text === undefined || text === null) {
        return _.cloneDeep(this[key]);
      } else {
        return _.filter(this[key], (item) => item.VAL.indexOf(text) > -1);
      }
    },

    /*
     check box vào nhân viên
    */
    checkbox(id) {
      let tmp = this.employeeList.find((item) => item.EmployeeId == id);
      tmp.checked = !tmp.checked;
      if (tmp.checked) this.employeeIdList.push(id);
      else {
        const index = this.employeeIdList.indexOf(id);
        if (index > -1) {
          this.employeeIdList.splice(index, 1);
        }
      }
    },

    /*
      Xóa nhân viên
    */
    removeEmployee() {
      let promiseList = [];
      this.employeeIdList.forEach((employeeId) =>
        promiseList.push(EmployeesAPI.delete(employeeId))
      );

      Promise.all(promiseList)
        .then((res) => {
          console.log(res);
        })
        .catch((err) => console.log(err))
        .finally(() => {
          this.employeeIdList = [];
          this.loadData();
        });
    },

    /*
      Mở form Employee Detail
      * @param:  formMode = 1 -> tạo mới nhân viên
                formMode = 0 -> hiện chi tiết nhân viên và có thể chỉnh sửa nhân viên
    */
    showForm(show = true) {
      //formMode = 1, employeeId = null
      this.isShowed = show;
    },

    /*
      Đóng mở toast message
    */
    showToast() {
      this.toast.isShowed = !this.toast.isShowed;
    },
  },
};
</script>

<style lang="css" scoped>
@import url("../../css/page/Employee.css");
</style>
