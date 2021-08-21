import BaseAPI from "@/api/base/BaseAPI.js";

class DepartmentAPI extends BaseAPI {
  constructor() {
    super();
    this.controller = "Departments";
  }
}

export default new DepartmentAPI();
