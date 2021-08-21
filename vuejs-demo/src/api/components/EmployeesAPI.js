import BaseAPI from "@/api/base/BaseAPI.js";
import BaseAPIConfig from "../base/BaseAPIConfig.js";

class EmployeesAPI extends BaseAPI {
  constructor() {
    super();
    this.controller = "Employees";
  }

  getNewEmployeeCode() {
    return BaseAPIConfig.get(`${this.controller}/NewEmployeeCode`);
  }

  getByFilterPaging(
    pageIndex,
    pageSize,
    employeeFilter = "",
    departmentId = "",
    positionId = ""
  ) {
    return BaseAPIConfig.get(
      `${this.controller}/Filter?pageIndex=${pageIndex}&pageSize=${pageSize}&employeeFilter=${employeeFilter}&departmentId=${departmentId}&positionId=${positionId}`
    );
  }
}

export default new EmployeesAPI();
