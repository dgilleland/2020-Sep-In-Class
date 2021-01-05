<form>
    <h1>Customer Details</h1>
    <label for="a">
      Customer Number
      <input id="a" type="text" aria-label="Customer Number" value="137" />
    </label>
    <label for="b">
      Name
      <input id="b" type="text" aria-label="Name" value="Fred Smith" />
    </label>
    <label for="c">
      Address
      <input id="c" type="text" aria-label="Address" value="123 Somewhere St." />
    </label>
    <label for="d">
      City
      <input id="d" type="text" aria-label="City" value="Edmonton" />
    </label>
    <label for="e">
      Province
      <input id="e" type="text" aria-label="Province" value="Alberta" />
    </label>
    <label for="f">
      Postal Code
      <input id="f" type="text" aria-label="Postal Code" value="T5H 2J9" />
    </label>
    <label for="g">
      Home Phone
      <input id="g" type="text" aria-label="Home Phone" value="436-7867" />
    </label>
    <button type="submit">Save</button>
</form>
<style>
form {
  display: grid;
}
input {
  right: 0;
}
label {
  display: flex;
}
button {
  width: fit-content;
  align-self: flex-end;
}
</style>