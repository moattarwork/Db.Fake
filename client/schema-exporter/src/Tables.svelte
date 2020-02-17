<script>
  import Loader from "./Loader.svelte";
  import { Card, CustomInput } from "sveltestrap";

  import { tables, tablesLoading } from "./stores";
</script>

{#if $tablesLoading}
  <Loader loading={$tablesLoading} />
{:else if $tables}
  <Card body color="light" class="m-2 rounded-0">
    <table class="table table-sm">
      <tbody>
        {#each $tables as table}
          <tr class="bg-secondary text-white">
            <td colspan="4">{table.name}</td>
          </tr>
          {#each table.fields as field}
            <tr>
              <td />
              <td>
                <CustomInput
                  type="checkbox"
                  id={field.name}
                  label={field.name}
                  bind:checked={field.checked} />
              </td>
              <td>
                {field.type}
                {#if field.typeLength}({field.typeLength}){/if}
              </td>
              <td style="width: 50%" />
            </tr>
          {/each}
        {/each}
      </tbody>
    </table>
  </Card>
{/if}
