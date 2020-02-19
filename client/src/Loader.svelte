<script>
  import Icon from 'svelte-awesome';
  import { refresh } from 'svelte-awesome/icons';

  export let loading = false;
</script>

{#if loading}
     <Icon data={refresh} spin/> Loading ...
{/if}




