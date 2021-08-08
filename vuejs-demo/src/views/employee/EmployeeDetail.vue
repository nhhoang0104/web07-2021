<template lang="html">
  <div>
    <base-modal :show="isShowed"></base-modal>
    <base-dialog :show="isShowed" @close-form="$emit('show-form', false)">
      <template #header>
        <div class="text text--heading">
          THÔNG TIN NHÂN VIÊN
        </div>
      </template>
      <template #body>
        <div style="width:25%; padding-right:10px;box-sizing: border-box">
          <div class="dialog__avatar"></div>
          <div class="text text--placeholder text--center">
            (Vui lòng chọn ảnh có định dạng .jpg, .jpeg,.png, .gif. )
          </div>
        </div>
        <div style="width:75%">
          <div style="padding-bottom:20px; display:inline-block">
            <div class="text text--title-2">A. THÔNG TIN CHUNG:</div>
            <div class="separator"></div>
            <div class="dialog__body__item">
              <base-input
                label="Mã nhân viên"
                id="EmployeeCode"
                ref="EmployeeCode"
                @handle-input="onChangeInput"
                :value="model['EmployeeCode']"
                required
              ></base-input>
            </div>
            <div class="dialog__body__item">
              <base-input
                label="Họ và tên"
                ref="FullName"
                placeholder="Nguyễn Văn A"
                id="FullName"
                @handle-input="onChangeInput"
                :value="model['FullName']"
                required
              ></base-input>
            </div>
            <div class="dialog__body__item">
              <base-input
                label="Ngày sinh"
                type="date"
                format="date"
                id="DateOfBirth"
                ref="DateOfBirth"
                :value="model['DateOfBirth']"
                @handle-input="onChangeInput"
              ></base-input>
            </div>
            <div class="dialog__body__item">
              <dropdown
                label="Giới tính"
                :value="model['Gender']"
                :data="gender"
                id="Gender"
                ref="Gender"
              >
                <template v-slot:dropdown-options>
                  <dropdown-option
                    v-for="item in gender"
                    :key="item.id"
                    :value="item.id"
                    :label="item.label"
                    :checked="item.id == model['Gender']"
                    @select-item="selectItem"
                  ></dropdown-option>
                </template>
              </dropdown>
            </div>
            <div class="dialog__body__item">
              <base-input
                label="Số CMTND/ Căn cước"
                placeholder="001200012345"
                id="IdentityNumber"
                :value="model['IdentityNumber']"
                @handle-input="onChangeInput"
                ref="IdentityNumber"
                required
              ></base-input>
            </div>
            <div class="dialog__body__item">
              <base-input
                label="Ngày cấp"
                type="date"
                format="date"
                id="IdentityDate"
                :value="model['IdentityDate']"
                @handle-input="onChangeInput"
                ref="IdentityDate"
              ></base-input>
            </div>
            <div class="dialog__body__item">
              <base-input
                label="Nơi cấp"
                placeholder="Hà Nội"
                ref="IdentityPlace"
                id="IdentityPlace"
                :value="model['IdentityPlace']"
                @handle-input="onChangeInput"
              ></base-input>
            </div>
            <div class="dialog__body__item"></div>
            <div class="dialog__body__item">
              <base-input
                label="Email"
                placeholder="example@gamil.com"
                format="email"
                :value="model['Email']"
                id="Email"
                ref="Email"
                @handle-input="onChangeInput"
                required
              ></base-input>
            </div>
            <div class="dialog__body__item">
              <base-input
                label="Số điện thoại"
                placeholder="0375783126"
                id="PhoneNumber"
                @handle-input="onChangeInput"
                :value="model['PhoneNumber']"
                ref="PhoneNumber"
                required
              ></base-input>
            </div>
          </div>
          <div style="padding-bottom:20px; display:inline-block">
            <div class="text text--title-2">A. THÔNG TIN CÔNG VIỆC:</div>
            <div class="separator"></div>
            <div class="dialog__body__item">
              <dropdown
                label="Vị trí"
                :value="model['PositionId']"
                :data="position"
                id="PositionId"
                ref="PositionId"
              >
                <template #dropdown-options="{ options }">
                  <dropdown-option
                    v-for="item in options"
                    :key="item.id"
                    :value="item.id"
                    :label="item.label"
                    :checked="item.id == model['PositionId']"
                    @select-item="selectItem"
                  ></dropdown-option>
                </template>
              </dropdown>
            </div>
            <div class="dialog__body__item">
              <dropdown
                label="Phòng ban"
                :value="model['DepartmentId']"
                :data="department"
                id="DepartmentId"
                ref="DepartmentId"
              >
                <template #dropdown-options="{options}">
                  <dropdown-option
                    v-for="item in options"
                    :key="item.id"
                    :value="item.id"
                    :label="item.label"
                    :checked="item.id == model['DepartmentId']"
                    @select-item="selectItem"
                  ></dropdown-option>
                </template>
              </dropdown>
            </div>
            <div class="dialog__body__item">
              <base-input
                label="Mã số thuế cá nhân"
                placeholder="54222457754"
                id="PersonalTaxCode"
                :value="model['PersonalTaxCode']"
                @handle-input="onChangeInput"
                ref="PersonalTaxCode"
              ></base-input>
            </div>
            <div class="dialog__body__item">
              <base-input
                label="Mức lương cơ bản"
                placeholder="25.000.000"
                ref="Salary"
                format="money"
                :value="model['Salary']"
                id="Salary"
                @handle-input="onChangeInput"
              ></base-input>
            </div>
            <div class="dialog__body__item">
              <base-input
                label="Ngày gia nhập công tin"
                type="date"
                format="date"
                :value="model['JoinDate']"
                id="JoinDate"
                ref="JoinDate"
                @handle-input="onChangeInput"
              ></base-input>
            </div>
            <div class="dialog__body__item">
              <dropdown
                label="Trạng tháu công việc"
                :value="model['WorkStatus']"
                :data="workStatus"
                id="WorkStatus"
                ref="WorkStatus"
                className="dropdown__select--top"
              >
                <template #dropdown-options="{options}">
                  <dropdown-option
                    v-for="item in options"
                    :key="item.id"
                    :value="item.id"
                    :label="item.label"
                    :checked="item.id == model['WorkStatus']"
                    @select-item="selectItem"
                  ></dropdown-option>
                </template>
              </dropdown>
            </div>
          </div>
        </div>
      </template>
      <template #footer>
        <div
          class="dialog__footer__btn btn--cancel"
          @click="$emit('show-form', false)"
        >
          Huỷ
        </div>
        <div class="dialog__footer__btn btn--done" @click="handleSubmit">
          Lưu
        </div>
      </template>
    </base-dialog>
  </div>
</template>

<script>
import { EmployeeModel } from "../../models/EmployeeModel";
import EmployeesAPI from "@/api/components/EmployeesAPI.js";
import _ from "lodash";

export default {
  props: {
    isShowed: { type: Boolean, required: true, default: false },
    formMode: { type: Number, required: true, default: 1 },
    employeeId: { type: String, required: false, default: null },
    department: { type: Array, required: true },
    position: { type: Array, required: true },
  },

  emits: ["show-form", "submit-form"],

  data() {
    return {
      model: _.cloneDeep(EmployeeModel),
      gender: [
        { id: "0", label: "Nữ" },
        { id: "1", label: "Nam" },
        { id: "2", label: "Không xác định" },
      ],
      workStatus: [
        { id: "0", label: "Thất nghiệp", checked: false },
        { id: "1", label: "Đang làm việc", checked: false },
      ],
      isShow: false,
    };
  },

  watch: {
    isShowed(newVal) {
      if (newVal === true) {
        // Clear dialog and auto focus vào input nhập mã nhân viên

        this.clearDialog();
        this.$refs.EmployeeCode.focusInput();

        if (this.formMode === 1) {
          this.getNewEmployeeCode();
        }

        if (this.formMode === 0) {
          this.getEmployeeInfo();
        }
      }

      if (newVal === false) {
        this.model = _.cloneDeep(EmployeeModel);
      }
    },

    model: {
      deep: true,
      handler() {},
    },
  },

  methods: {
    /*
      Lấy mã nhân viên mới
    */
    getNewEmployeeCode() {
      EmployeesAPI.getNewEmployeeCode()
        .then((res) => {
          this.model.EmployeeCode = res.data;
        })
        .catch((err) => {
          console.log(err.message);
        });
    },

    /*
      Lấy thông tin mã nhân viên theo id
    */
    getEmployeeInfo() {
      EmployeesAPI.getById(this.employeeId).then((res) => {
        if (typeof res.data === "object") this.model = res.data;
      });
    },

    /*
        Xử lý chọn option trong cac combobox
    */
    selectItem(item) {
      this.model[item.key] = item.id;
    },

    /*
    ` Xử lý onchangeinput
    */
    onChangeInput({ id, value }) {
      this.model[id] = value;
    },

    /**
      Xử lý submit form
      Thêm mới nhân viên hoặc chỉnh sửa thông tin nhân viên
      Nếu isValidted thì submit, ngược lại thì hiện thị tooltip lỗi
      trả về promise tương ứng để thực hiện ở Component EmployeeList
    */
    handleSubmit() {
      let isValidated = true;

      Object.keys(this.$refs).forEach((el) => {
        this.$refs[el].validate();
        if (!this.$refs[el].isValidated)
          isValidated = this.$refs[el].isValidated;
      });

      if (isValidated) {
        let promise = null;

        if (this.formMode === 1) {
          promise = EmployeesAPI.add(this.model);
        }

        if (this.formMode === 0) {
          promise = EmployeesAPI.update(this.employeeId, this.model);
        }

        this.$emit("submit-form", { action: promise, type: this.formMode });
      }
    },

    /**
     * Clear tooltip, danger khi mở dialog
     */
    clearDialog() {
      Object.keys(this.$refs).forEach((el) => {
        this.$refs[el].clearTooltip();
      });
    },
  },
};
</script>

<style lang="css" scoped></style>
