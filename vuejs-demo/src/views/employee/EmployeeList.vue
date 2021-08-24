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
          @handle-input="handleInput"
          :value="search.employeeFilter"
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
            <base-table-head
              :columns="columns"
              :checked="isSelectedAll"
              @select-all="selectAll"
            ></base-table-head>
          </template>
          <template v-slot:tbody>
            <base-table-body
              :columns="columns"
              :data="employeeList"
              :employeeDeleteList="employeeIdList"
              @check-box="checkbox"
              @dblclick="viewInfo"
            ></base-table-body>
          </template>
        </base-table>
      </div>
      <div class="scrollbar__hide"></div>
    </div>
    <div class="content__footer">
      <base-pagination
        :currentPage="currentPage"
        :totalRecord="totalRecord"
        :totalPages="totalPages"
        :pageSize="pageSize"
        @select-page="selectPageIndex"
        @select-size="selectPageSize"
      ></base-pagination>
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
      v-for="(item,index) in toastList"
      :index="index"
      :key="item.id"
      :type="item.type"
      :message="item.message"
      @close="closeToast(item.id)"
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
import { v4 as uuidv4 } from "uuid";

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
        departmentId: "",
        positionId: "",
        employeeFilter: "",
      },

      isLoading: false,

      dialog: { employeeId: null, isShowed: false, formMode: 1 },

      toastList: [],

      popup: {
        type: "done",
        title: "Xoa nhan vien",
        content: "Tải dữ liệu thành công",
        isShowed: false,
        action: null,
        cancel: null,
      },

      currentPage: 1,

      totalRecord: 20, // tổng số bản ghi

      totalPages: 10, // tổng số index trang

      pageSize: 10,
    };
  },

  created() {
    /*
        Lấy dữ liệu các vị trí, các phòng ban, nhân viên từ api
    */
    this.isLoading = true;

    Promise.all([
      EmployeesAPI.getByFilterPaging(
        this.currentPage,
        this.pageSize,
        this.search.employeeFilter,
        this.search.departmentId,
        this.search.positionId
      ),
      PositionAPI.getAll(),
      DepartmentAPI.getAll(),
    ])
      .then((res) => {
        this.employeeList = res[0].data.employees;
        this.totalPages = res[0].data.totalPage;
        this.totalRecord = res[0].data.totalRecord;
        this.page;

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
        this.positionCbb.push({ id: "", label: "Tất cả vị trí" });

        this.departmentCbb = _.cloneDeep(this.department);
        this.departmentCbb.push({ id: "", label: "Tất cả phòng ban" });
        this.isLoading = false;
        this.setToast("done", "Tải dữ liệu thành công");
      })
      .catch((err) => {
        console.log(err);
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

    employeeIdList: {
      deep: true,
      handler() {},
    },
  },

  computed: {
    isSelectedAll() {
      if (this.employeeIdList.length === 0) return false;

      var isSelectedAll = true;

      this.employeeList.forEach((employee) => {
        if (this.employeeIdList.indexOf(employee.EmployeeId) === -1) {
          isSelectedAll = false;
        }
      });

      return isSelectedAll;
    },
  },

  methods: {
    /**
        Lấy dữ liệu nhân viên từ api
    */
    loadData() {
      this.isLoading = true;
      EmployeesAPI.getByFilterPaging(
        this.currentPage,
        this.pageSize,
        this.search.employeeFilter,
        this.search.departmentId,
        this.search.positionId
      )
        .then((res) => {
          console.log(res);
          this.employeeList = res.data.employees;
          this.totalPages = res.data.totalPage;
          this.totalRecord = res.data.totalRecord;
          this.isLoading = false;
          this.setToast("done", "Tải dữ liệu thành công");
        })
        .catch((err) => {
          console.log(err);
          this.setToast("danger", "Tải dữ liệu thất bại");
        });
    },

    /**
      Xử lý chọn option trong cac combobox
    */
    selectItem(item) {
      this.search[item.key] = item.id;
      this.loadData();
    },

    /**
     check box vào nhân viên
    */
    checkbox(id) {
      let index = this.employeeIdList.indexOf(id);

      if (index === -1) this.employeeIdList.push(id);
      else {
        this.employeeIdList.splice(index, 1);
      }
    },

    /**
        Hiện cảnh báo(popup) muốn thực hiện hành đọng hay không
    */
    preRemoveEmployee() {
      let message = "Bạn có chắc chắn muốn xóa các nhân viên đã chọn không?";

      if (this.employeeIdList.length === 1) {
        let tmp = _.find(this.employeeList, {
          EmployeeId: this.employeeIdList[0],
        });

        message = `Bạn có chắc chắn muốn nhân viên ${tmp.EmployeeCode} đã chọn không?`;
      }

      this.setPopup(
        "Xóa các nhân viên",
        message,
        this.removeEmployees,
        this.uncheckEmployees
      );
    },

    /**
      Xóa nhân viên
    */
    removeEmployees() {
      if (this.employeeIdList.length === 1) {
        this.isLoading = true;
        EmployeesAPI.delete(this.employeeIdList[0])
          .then(() => {
            this.setToast("done", "Xóa nhân viên thành công");
          })
          .catch(() => {
            this.setToast("danger", "Xóa nhân viên thất bại!");
          })
          .finally(() => {
            this.employeeIdList = [];
            this.loadData();
          });
      }

      if (this.employeeIdList.length > 1) {
        this.isLoading = true;
        EmployeesAPI.deleteList(_.cloneDeep(this.employeeIdList))
          .then(() => {
            this.setToast("done", "Xóa nhiều nhân viên thành công");
          })
          .catch(() => {
            this.setToast("danger", "Xóa nhiều nhân viên thất bại!");
          })
          .finally(() => {
            this.employeeIdList = [];
            this.loadData();
          });
      }
    },

    /**
      Bỏ tất cả các các checkbox của các nhân viên đã chọn
    */
    uncheckEmployees() {
      this.employeeIdList = [];
    },

    /**
      Xem thông tin chi tiết nhân viên
    */
    viewInfo(id) {
      this.dialog = { employeeId: id, formMode: 0, isShowed: true };
    },

    /**
      Thêm mới nhân viên
    */
    addNewEmployee() {
      this.dialog = { formMode: 1, isShowed: true };
    },

    /**
      Mở form Employee Detail
      * @param:  formMode = 1 -> tạo mới nhân viên
                formMode = 0 -> hiện chi tiết nhân viên và có thể chỉnh sửa nhân viên
    */
    showForm(show = true) {
      //formMode = 1, employeeId = null
      this.dialog.isShowed = show;
    },

    /**
        Truyền nội dùng toast message
    */
    setToast(type, message) {
      this.toastList.push({
        id: uuidv4(),
        type: type,
        message: message,
        isShowed: true,
      });
    },

    /** 
      Đóng toast message
    */
    closeToast(id) {
      this.toastList = this.toastList.filter((item) => {
        return !(item.id === id);
      });
    },

    /**
        Truyền nội dung popup
    */
    setPopup(title, content, action, cancel) {
      this.popup = { title, content, isShowed: true, action, cancel };
    },

    /**
      Đóng mở popup
    */
    showPopup(isShowed) {
      this.popup.isShowed = isShowed;
    },

    /**
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

    /**
     * xử lý sự kiện chọn index trang
     */
    selectPageIndex(pageIndex) {
      this.currentPage = pageIndex;
      this.loadData();
    },

    /**
     * xử lý sự kiện chọn kích cỡ trang
     */
    selectPageSize(pageSize) {
      this.pageSize = pageSize;
      this.loadData();
    },

    /**
     * Xu ly text input
     */
    handleInput({ value }) {
      clearTimeout(this.timeoutItem);
      this.timeoutItem = setTimeout(() => {
        this.search.employeeFilter = value;
        this.loadData();
      }, 300);
    },

    /**
     * Chọn tất cả
     */
    selectAll(isCheckedAll = false) {
      if (!isCheckedAll) {
        this.employeeList.forEach((employee) => {
          if (this.employeeIdList.indexOf(employee.EmployeeId) === -1) {
            this.employeeIdList.push(employee.EmployeeId);
          }
        });
      } else {
        let tmp = _.map(this.employeeList, "EmployeeId");
        this.employeeIdList = this.employeeIdList.filter((id) => {
          return !tmp.includes(id);
        });
      }
    },
  },
};
</script>

<style lang="css" scoped>
@import url("../../css/page/Employee.css");
</style>
