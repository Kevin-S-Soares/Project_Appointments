export function Page(props) {
    let state;
    if (!props.fetched) {
        state = loading(props);
    }
    else {
        if (props.data.length === 0) {
            state = noData(props);
        }
        else {
            state = populateTable(props);
        }
    }
    return (
        <div>
            <a href={window.location.origin + "/Odontologist/Add"}>Add new odontologist</a>
            {state}
        </div>
    );
}

function loading(props) {
    return (
        <h5>Loading.</h5>
    );
}

function noData(props) {
    return (
        <h3>No data available.</h3>
    );
}

function populateTable(props) {
    const url = window.location.origin;
    return (
        <div>
            <table border={1}>
                <thead>
                    <tr>
                        <td><b>Id</b></td>
                        <td>Name</td>
                        <td>Phone</td>
                        <td>Email</td>
                        <td colSpan={3}>Actions</td>
                    </tr>
                </thead>
                <tbody>
                    {props.data.map((element, iteration) =>
                        <tr key={iteration}>
                            <td>{element.id}</td>
                            <td>{element.name}</td>
                            <td>{element.phone}</td>
                            <td >{element.email}</td>
                            <td><a href={url + "/Odontologist/Details/" + element.id}>Details</a></td>
                            <td><a href={url + "/Odontologist/Edit/" + element.id}>Edit</a></td>
                            <td><a href={url + "/Odontologist/Remove/" + element.id}>Remove</a></td>
                        </tr>
                    )}
                </tbody>
            </table>
        </div>
    );
}