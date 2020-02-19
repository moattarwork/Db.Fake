<script>
  import { Button } from "sveltestrap";
  import Icon from "svelte-awesome";
  import * as icons from "svelte-awesome/icons";

  import * as api from "./api";
  import * as store from "./stores";

  export let database = null;
  export let connection = "";

  async function loadTables() {
    store.tablesLoading.set(true);
    const tables = await api.loadTables(connection, database.name);
    store.selectedDatabase.set(database.name);
    store.tables.set(tables);
    store.tablesLoading.set(false);
  }
</script>

{#if database}
  <a href="javascript:void(0)" on:click={loadTables}>
    <Icon data={icons.database} />
    {database.name}
  </a>
{/if}
