import axios from 'axios';

// Get请求方法
const get = (url, params) => {
  return axios.get(url, { params }).then(res => res.data);
};
// Post请求方法
const post = (url, data) => {
  return axios.post(url, data).then(res => res.data);
};
// 其他方法
const other = (method, url, data) => {
  return axios({
    method: method,
    url: url,
    data,
    traditional: true
  }).then(res => res.data);
};

export {
  get,
  post,
  other
}
