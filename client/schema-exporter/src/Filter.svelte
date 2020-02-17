<script>
  import {
    Card,
    CardBody,
    Label,
    Input,
    Form,
    FormGroup,
    Button
  } from "sveltestrap";

  import * as api from "./api";
  import * as store from "./stores.js";

  import Loader from "./Loader.svelte";

  let connectionString = "";
  let loading = false;

  async function handleLoad() {
    store.connection.set(connectionString);

    loading = true;
    const databases = await api.loadDatabases(connectionString);
    store.databases.set(databases);
    loading = false;
  }
</script>

<Card body color="light" class="m-2 rounded-0">
  <div class="row">
    <div class="col-1 text-nowrap">
      <span class="mr-sm-2">Connection</span>
    </div>
    <div class="col-7">
      <Input
        type="text"
        bind:value={connectionString}
        id="connectionString"
        placeholder="Add connection string here ..." />
    </div>
    <div class="col-2">
      <Button color="primary" on:click={handleLoad}>Load schema</Button>
    </div>
    <div class="col-2">
      <Loader {loading} />
    </div>

  </div>
</Card>
