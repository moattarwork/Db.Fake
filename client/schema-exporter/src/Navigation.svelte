<script>
  import Database from "./Database.svelte";
  import { Card } from "sveltestrap";

  import { connection, databases } from "./stores";
</script>

<Card body color="light" class="m-2 rounded-0">
  {#if $databases}
    {#each $databases as database}
      <Database {database} connection={$connection} />
    {/each}
  {/if}
</Card>
