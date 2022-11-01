import axios from 'axios';

// 登录请求方法
const get = (url, params) => {
  return axios.get(url, {
    method: "GET",
    headers: {
      'Content-Type': 'application/x-www-form-urlencoded',
    },
    traditional: true,
    transformRequest: [
      function (data) {
        let ret = ''
        for (let it in data) {
          ret +=
            encodeURIComponent(it) +
            '=' +
            encodeURIComponent(data[it]) +
            '&'
        }
        return ret
      }
    ]
  }).then(res => res.data);
};
// 通用公用方法
const req = (method, url, data) => {
  return axios({
    method: method,
    url: url,
    data,
    traditional: true
  }).then(res => res.data);
};

export {
  get,
  req
}
