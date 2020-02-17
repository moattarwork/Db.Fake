<script>
	import { Col, Container, Row } from "sveltestrap";
	import {
		Collapse,
		Navbar,
		NavbarToggler,
		NavbarBrand,
		Nav
	} from 'sveltestrap';

	let isOpen = false;

	function handleUpdate(event) {
		isOpen = event.detail.isOpen;
    }
    
    export let title;
</script>

<Navbar color="dark" dark expand="md">
  <NavbarBrand href="/">{title}</NavbarBrand>
  <NavbarToggler on:click={() => (isOpen = !isOpen)} />
  <Collapse {isOpen} navbar expand="md" on:update={handleUpdate} />
</Navbar>