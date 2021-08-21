import BaseAPIConfig from "../base/BaseAPIConfig.js";

export default class BaseAPI {
  constructor() {
    this.controller = null;
  }
  /**
   * Phương thức lấy tất cả dữ liệu
   *
   */
  getAll() {
    return BaseAPIConfig.get(`${this.controller}`);
  }

  /**
   * Phương thức thêm mới nhân với
   * @param {*} body
   */
  add(body) {
    return BaseAPIConfig.post(`${this.controller}`, body);
  }

  /**
   * Phương thức lấy dữ liệu  theo id
   * @param {*} id
   */

  getById(id) {
    return BaseAPIConfig.get(`${this.controller}/${id}`);
  }

  /**
   * Hàm lấy dữ liệu phân trang
   * @param {*} payload
   */
  paging(payload) {
    return BaseAPIConfig.post(`${this.controller}/paging`, payload);
  }

  /**
   * Hàm cập nhật dữ liệu
   * @param {*} id
   * @param {*} body
   */
  update(id, body) {
    return BaseAPIConfig.put(`${this.controller}/${id}`, body);
  }

  /**
   * Hàm xóa bản ghi
   * @param {*} id
   */
  delete(id) {
    return BaseAPIConfig.delete(`${this.controller}/${id}`);
  }

  /**
   * Hàm xóa nhieu bản ghi
   */
  deleteList(idList) {
    let tmp = "";
    idList.forEach((id) => {
      tmp += `entitiesId=${id}&`;
    });
    return BaseAPIConfig.delete(`${this.controller}?${tmp}`);
  }
}
