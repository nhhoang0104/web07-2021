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
          @onclick="preRemoveEmployee"
        >
          <div style="margin-right:8px">
            <i class="icon icon--13 far fa-trash-alt"></i>
          </div>
          <div class="text text--color-white text--center">
            Xóa nhân viên
          </div>
        </base-button>
        <base-button
          className="padding-left--16 padding-right--16"
          @onclick="addNewEmployee"
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
          value=""
          id="searchText"
        ></base-input>
        <combo-box
          :data="departmentCbb"
          :value="search['departmentId']"
          id="departmentId"
        >
          <template #combo-box-options="{options}">
            <combo-box-option
              v-for="item in options"
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
          :data="positionCbb"
          id="positionId"
        >
          <template #combo-box-options="{options}">
            <combo-box-option
              v-for="item in options"
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
              @dblclick="viewInfo"
            ></base-table-body>
          </template>
        </base-table>
      </div>
    </div>
    <div class="content__footer">
      <base-pagination></base-pagination>
    </div>
    <base-loader :is-loading="isLoading"></base-loader>
    <EmployeeDetail
      :is-showed="dialog.isShowed"
      :form-mode="dialog.formMode"
      :department="department"
      :position="position"
      :employee-id="dialog.employeeId"
      @show-form="showForm"
      @submit-form="handleSubmitForm"
    />
    <base-toast-message
      :type="toast.type"
      :message="toast.message"
      :isShowed="toast.isShowed"
      @close="showToast"
    ></base-toast-message>
    <base-popup :popup="popup" @show="showPopup"></base-popup>
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
      departmentCbb: [],
      positionCbb: [],
      search: {
        departmentId: "1",
        positionId: "1",
        textSearch: "",
      },
      isLoading: false,
      dialog: { employeeId: null, isShowed: false, formMode: 1 },
      toast: {
        type: "done",
        message: "Tải dữ liệu thành công",
        isShowed: false,
      },
      popup: {
        type: "done",
        title: "Xoa nhan vien",
        content: "Tải dữ liệu thành công",
        isShowed: false,
        action: null,
        cancel: null,
      },
    };
  },

  created() {
    /*
        Lấy dữ liệu các vị trí, các phòng ban, nhân viên từ api
    */
    this.isLoading = true;

    Promise.all([
      EmployeesAPI.getAll(),
      PositionAPI.getAll(),
      DepartmentAPI.getAll(),
    ])
      .then((res) => {
        this.employeeList = res[0].data;

        this.position = res[1].data.map(({ PositionId, PositionName }) => ({
          id: PositionId,
          label: PositionName,
        }));

        this.department = res[2].data.map(
          ({ DepartmentId, DepartmentName }) => ({
            id: DepartmentId,
            label: DepartmentName,
          })
        );

        // Khởi tạo data cho combobox
        this.positionCbb = _.cloneDeep(this.position);
        this.positionCbb.push({ id: "1", label: "Tất cả vị trí" });

        this.departmentCbb = _.cloneDeep(this.department);
        this.departmentCbb.push({ id: "1", label: "Tất cả phòng ban" });
        this.isLoading = false;
        this.setToast("done", "Tải dữ liệu thành công");
      })
      .catch((err) => {
        console.log(err.message);
        this.setToast("danger", "Tải dữ liệu thất bại");
      });
  },

  watch: {
    isLoading(newVal) {
      if (newVal === true) {
        this.departmentForm = _.cloneDeep(this.department);
        this.positionForm = _.cloneDeep(this.position);
      }
    },

    department: {
      deep: true,
      handler() {},
    },

    position: {
      deep: true,
      handler() {},
    },

    toast: {
      deep: true,
      handler() {},
    },

    dialog: {
      deep: true,
      handler() {},
    },
  },

  methods: {
    /*
        Lấy dữ liệu nhân viên từ api
    */
    loadData() {
      this.isLoading = true;
      EmployeesAPI.getAll()
        .then((res) => {
          this.employeeList = res.data;
          this.isLoading = false;
        })
        .catch((err) => console.log(err));
    },

    /*
      Xử lý chọn option trong cac combobox
    */
    selectItem(item) {
      this.search[item.key] = item.id;
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
        Hiện cảnh báo(popup) muốn thực hiện hành đọng hay không
    */

    preRemoveEmployee() {
      this.setPopup(
        "Xóa các nhân viên",
        "Bạn có chắc chắn muốn xóa các nhân viên đã chọn không?",
        this.removeEmployees,
        this.uncheckEmployees
      );
    },

    /*
      Xóa nhân viên
    */
    removeEmployees() {
      let promiseList = [];
      this.employeeIdList.forEach((employeeId) =>
        promiseList.push(EmployeesAPI.delete(employeeId))
      );

      this.isLoading = true;
      Promise.all(promiseList)
        .then(() => {
          this.setToast("done", "Xóa nhân viên thành công");
        })
        .catch((err) => {
          console.log(err.message);
          this.setToast("danger", "Xóa nhân viên thất bại!");
        })
        .finally(() => {
          this.employeeIdList = [];
          this.loadData();
        });
    },

    /*
      Bỏ tất cả các các checkbox của các nhân viên đã chọn
    */
    uncheckEmployees() {
      // this.employeeIdList = [];
    },

    /*
      Xem thông tin chi tiết nhân viên
    */
    viewInfo(id) {
      this.dialog = { employeeId: id, formMode: 0, isShowed: true };
    },

    /*
      Thêm mới nhân viên
    */
    addNewEmployee() {
      this.dialog = { formMode: 1, isShowed: true };
    },

    /*
      Mở form Employee Detail
      * @param:  formMode = 1 -> tạo mới nhân viên
                formMode = 0 -> hiện chi tiết nhân viên và có thể chỉnh sửa nhân viên
    */
    showForm(show = true) {
      //formMode = 1, employeeId = null
      this.dialog.isShowed = show;
    },

    /*
        Truyền nội dùng toast message
    */
    setToast(type, message) {
      this.toast = { type: type, message: message, isShowed: true };
    },

    /*
      Đóng mở toast message
    */
    showToast(isShowed) {
      this.toast.isShowed = isShowed;
    },

    /*
        Truyền nội dung popup
    */
    setPopup(title, content, action, cancel) {
      this.popup = { title, content, isShowed: true, action, cancel };
    },

    /*
      Đóng mở popup
    */
    showPopup(isShowed) {
      this.popup.isShowed = isShowed;
    },

    /*
        Xử lý khi ấn submit của form
    */
    handleSubmitForm({ action, type }) {
      action
        .then(() => {
          this.setToast(
            "done",
            type === 1
              ? "Thêm mới nhân viên thành công"
              : "Chỉnh sửa thông tin thành công"
          );
        })
        .catch((err) => {
          console.log(err.message);
          this.setToast(
            "danger",
            type === 1
              ? "Thêm mới nhân viên thất bại"
              : "Chỉnh sửa thông tin thất bại"
          );
        })
        .finally(() => {
          this.showForm(false);
          this.loadData();
        });
    },
  },
};
</script>

<style lang="css" scoped>
@import url("../../css/page/Employee.css");
</style>
