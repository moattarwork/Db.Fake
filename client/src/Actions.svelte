<script>
  import { Card, Button } from "sveltestrap";

  import { connection, selectedDatabase, tables } from "./stores";
  import * as api from "./api";

  async function exportMaskPlan() {
    if ($connection && $selectedDatabase && $tables) {
      const maskedFields = $tables.map(t => {
        return {
          tableName: `[${t.schema}].[${t.name}]`,
          tableFields: t.fields.filter(f => f.checked).map(f => f.name)
        };
      });

      await api.exportPlan($connection, $selectedDatabase, maskedFields);
    }
  }
</script>

<Card body color="light" class="m-2 rounded-0">
  <Button on:click={exportMaskPlan}>Export Mask Plan</Button>
</Card>
