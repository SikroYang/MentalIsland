<!--
 * @Author: error: git config user.name && git config user.email & please set dead value or install git
 * @Date: 2022-10-28 09:24:55
 * @LastEditors: error: git config user.name && git config user.email & please set dead value or install git
 * @LastEditTime: 2022-11-08 10:18:55
 * @FilePath: \project\pages\psychology.vue
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
-->
<template>
  <div>
    <Top />
    <div class="center col">
      <h1>双相情感障碍的前世今生</h1>
      <div>
        <p>
          双相情感障碍其实并不是近现代才产生的新型疾病，早在古希腊时期，就有医生记载道“一群人时而不停歌唱、跳舞，大笑，时而却立马陷入悲伤、懒散”，这种从动力高涨到动力减退的现象，在古代就已经存在，只不过现代医学没有发展完善，还不足以用科学的诊断标准，这样的现象也仅仅是一种异常的行为而已。
        </p>
        <p>
          然而随着时代的发展，医生发现有这样行为的人，总是伴随着精神问题，已经不能简单用异常行为来描述了，由于病患时而情绪高涨、时而低落，这与单向的情绪变化不同，而是一种情绪在双相之间的变化，于是在1845年“双相障碍”的概念被正式提出。
        </p>
        <p>
          即使是概念被提出，但是诊断标准和疾病的归属依然不明晰，在1980年，双相情感障碍才被纳入美国的精神诊断当中，这象征着双相障碍不仅仅是一种心理异常，有可能需要从生理、精神的层面来解释双相。
        </p>
        <p>
          直到2013年以前，第三版、第四版美国精神障碍诊断手册中，还是将双相障碍和重度抑郁归属于同类的“心境障碍”之中。这时人们还是偏向双相与抑郁较为类似。
        </p>
        <p>
          但在最新一版的美国精神障碍诊断手册中，双相终于有了更明晰的医学谱系，他从“抑郁障碍”中分离，成为精神分裂症相关疾病的体现。
        </p>
        <p>
          精神疾病的诊断是一个不断完善的过程，双相从简单的异常行为到重度精神障碍的诊断，经历了千年，也许随着未来科技的发展，还能作出更准确的判断与归类。
        </p>
      </div>
    </div>
  </div>
</template>
    
    <script>
import Top from "../components/Header.vue";
export default {
  name: "PsyDetailsPage",
  components: { Top },
  data() {
    return {};
  },
  methods: {
    research() {
      let that = this;
      console.log(that.reInput);
    },
  },
};
</script>
  <style scoped>
h1,
h3 {
  color: #9f7861;
}
.col {
  color: #9f7861;
}
.flex {
  display: flex;
  width: 40%;
  margin: auto;
}
.j-c {
  display: flex;
  width: 90%;
  margin: auto;
  margin-top: 50px;
  justify-content: space-between;
}
.mar {
  margin-left: 20px;
}
.mart {
  margin-top: 30px;
  text-align: center;
}
.center {
  width: 60%;
  margin: auto;
  margin-top: 60px;
}
.center h1 {
  text-align: center;
}
.pict {
  width: 100%;
  height: 220px;
}
.border {
  border: 1px solid red;
}
.juc {
  width: 30%;
}
.textl {
  text-align: left;
}
</style>
    