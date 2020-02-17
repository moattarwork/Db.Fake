<script>
  import Header from "./Header.svelte";
  import Filter from "./Filter.svelte";
  import Navigation from "./Navigation.svelte";
  import Tables from "./Tables.svelte";
  import Actions from "./Actions.svelte";
  import { Col, Container, Row } from "sveltestrap";
</script>

<Header title="Database Schema Browser" />

<Container fluid>
  <Row>
    <Col>
      <Filter />
    </Col>
  </Row>
  <Row>
    <Col md={3}>
      <Navigation />
    </Col>
    <Col md={9}>
      <Tables />
      <Actions />
    </Col>
  </Row>
</Container>
