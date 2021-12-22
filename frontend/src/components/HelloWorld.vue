<template>
  <div class="main-wrap">
    <h2 class="title">
      Shorten your long URL, <br />
      or create a custom one.
    </h2>

    <div v-if="loading" class="loading">
      <Loading />
    </div>

    <div v-else class="shorten-form">
      <div class="input-group flex-nowrap url-input">
        <span class="input-group-text">URL:</span>
        <input
          id="longUrl"
          type="text"
          class="form-control"
          placeholder="Enter your long URL here..."
          aria-label="Enter your long URL here..."
          aria-describedby="addon-wrapping"
          v-model="longUrl"
        />
      </div>
      <div class="input-group mb-3">
        <label class="input-group-text">Category:</label>
        <select
          class="form-select"
          id="inputGroupSelect"
          v-model="selectedGroup"
        >
          <option v-for="group in groups" :key="group.id" :value="group.id">
            {{ group.name }}
          </option>
        </select>
      </div>
      <div class="form-check">
        <input
          class="form-check-input"
          type="checkbox"
          value=""
          id="flexCheckDefault"
          @click="customUrl()"
        />
        <label class="form-check-label" for="flexCheckDefault">
          Custom short URL
        </label>
      </div>
      <div v-if="custom" class="input-group flex-nowrap url-input">
        <span class="input-group-text" id="shortUrl">localhost:7084/</span>
        <input
          type="text"
          class="form-control"
          placeholder="Enter your custom shortened URL here..."
          aria-label="Enter your custom shortened URL here..."
          aria-describedby="addon-wrapping"
        />
      </div>
      <button type="button" class="btn btn-success" @click="postData()">
        Generate
      </button>
    </div>
  </div>
</template>

<script lang="js">
import Loading from "@/components/Loading";

export default({
  name: 'HelloWorld',
  components: {Loading},
  data() {
    return {
      loading: false,
      custom: false,
      longUrl: '',
      selectedGroup: ''
    };
  },
  computed: {
    groups() {
      console.log(this.$store.getters.getGroups)
      return this.$store.getters.getGroups;
    }
  },
  mounted() {
    this.$store.dispatch("loadGroups");
  },
  methods: {
    customUrl: function() {
      console.log(this.custom);
      this.custom = !this.custom;
    },
    postData() {
      const urlData  = {};
      urlData['url'] = this.longUrl;
      urlData['groupId'] = this.selectedGroup;
      this.$store.dispatch("postUrl", urlData);
    }
  },
});
</script>

<style>
.main-wrap {
  width: 700px;
  height: auto;
  background-color: #fff;
  margin: 0 auto;
  border-radius: 12px 12px;
}

.shorten-form {
  padding: 2em;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: flex-start;
}

.url-input {
  margin-bottom: 1em;
}

.form-check {
  display: inline-block;
  margin-bottom: 1em;
}

.btn {
  display: block;
}

.title {
  font-weight: bold;
  padding: 1em 0 0 0;
}
</style>
