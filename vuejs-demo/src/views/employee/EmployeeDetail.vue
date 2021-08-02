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
        <div style="width:25%">
          <div class="dialog__avatar"></div>
          <div style="text-align:center">
            (Vui lòng chọn ảnh có định dạng .jpg, .jpeg,.png, .gif. )
          </div>
        </div>
        <div style="width:75%">
          <div style="padding-bottom:20px; display:inline-block">
            <div class="text text--title-2">A. THÔNG TIN CHUNG:</div>
            <div class="separator"></div>
            <div class="dialog__body__item">
              <base-input label="Mã nhân viên" required></base-input>
            </div>
            <div class="dialog__body__item">
              <base-input
                label="Họ và tên"
                placeholder="Nguyễn Văn A"
                required
              ></base-input>
            </div>
            <div class="dialog__body__item">
              <base-input
                label="Ngày sinh"
                type="date"
                format="date"
              ></base-input>
            </div>
            <div class="dialog__body__item">
              <dropdown
                label="Giới tính"
                :value="model['Gender']"
                :data="gender"
                id="Gender"
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
                required
              ></base-input>
            </div>
            <div class="dialog__body__item">
              <base-input
                label="Ngày cấp"
                type="date"
                format="date"
              ></base-input>
            </div>
            <div class="dialog__body__item">
              <base-input label="Nơi cấp" placeholder="Hà Nội"></base-input>
            </div>
            <div class="dialog__body__item"></div>
            <div class="dialog__body__item">
              <base-input
                label="Email"
                placeholder="example@gamil.com"
                format="email"
                required
              ></base-input>
            </div>
            <div class="dialog__body__item">
              <base-input
                label="Số điện thoại"
                placeholder="0375783126"
                required
              ></base-input>
            </div>
          </div>
          <div style="padding-bottom:20px; display:inline-block">
            <div class="text text--title-2">A. THÔNG TIN CÔNG VIỆC:</div>
            <div class="separator"></div>
            <div class="dialog__body__item">
              <dropdown
                label="Giới tính"
                :value="model['Gender']"
                :data="gender"
                id="Gender"
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
              <dropdown
                label="Giới tính"
                :value="model['Gender']"
                :data="gender"
                id="Gender"
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
                label="Mã số thuế cá nhân"
                placeholder="54222457754"
              ></base-input>
            </div>
            <div class="dialog__body__item">
              <base-input
                label="Mức lương cơ bản"
                placeholder="25.000.000"
              ></base-input>
            </div>
            <div class="dialog__body__item">
              <base-input
                label="Ngày gia nhập công tin"
                type="date"
                format="date"
              ></base-input>
            </div>
            <div class="dialog__body__item">
              <dropdown
                label="Giới tính"
                :value="model['WorkStatus']"
                :data="workStatus"
                id="WorkStatus"
              >
                <template v-slot:dropdown-options>
                  <dropdown-option
                    v-for="item in workStatus"
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
        <div
          class="dialog__footer__btn btn--done"
          @click="$emit('show-form', false)"
        >
          Xóa
        </div>
      </template>
    </base-dialog>
  </div>
</template>

<script>
import { EmployeeModel } from "../../models/EmployeeModel";
import _ from "lodash";

export default {
  props: {
    isShowed: { type: Boolean, required: true, default: false },
    formMode: { type: Number, required: true, default: 1 },
    employeeId: { type: String, required: false, default: null },
    department: { type: Array, required: true },
    position: { type: Array, required: true },
  },
  emits: ["show-form"],
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
      departmentForm: _.cloneDeep(this.department),
      positionForm: _.cloneDeep(this.position),
      dataForm: {},
      isShow: false,
    };
  },
  methods: {
    /*
        Xử lý chọn option trong cac combobox
    */
    selectItem(item) {
      this.model[item.key] = item.id;
    },
  },
};
</script>

<style lang="css"></style>
