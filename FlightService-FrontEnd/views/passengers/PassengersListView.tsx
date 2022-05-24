import { AxiosResponse } from "axios";
import React, { ChangeEvent, FormEvent, MouseEvent } from "react";
import { Button, Form, Modal } from "react-bootstrap";
import Passenger, { AddPassenger } from "../../models/passenger";
import APIService from "../../services/apiService";


type PassengersListViewProps = {

}

type PassengersListViewState = {
    showAdd: boolean;
    passengerId: number;
    name: string;
    job: string;
    email: string;
    age: string;
    passengers: Passenger[];
    showEdit: boolean;
    flightId: number;
    showAddFlight: boolean;
}

export class PassengersListView extends React.Component<PassengersListViewProps, PassengersListViewState> {
    constructor(props: PassengersListViewProps) {
        super(props);
        this.state = {
            showAdd: false,
            passengerId: 0,
            name: "",
            job: "",
            email: "",
            age: "",
            passengers: [],
            showEdit: false,
            showAddFlight: false,
            flightId: 0
        }
    }
    componentDidMount() {
        APIService.getPassengers()
            .then((response) => {
                this.setState({
                    passengers: response.data
                });
            })
            .catch((err: Error) => {
                // console.log(err);
                alert(err);
            });
    }
    componentDidUpdate(pProps: PassengersListViewProps, pState: PassengersListViewState) {
        if (pState.passengers.length !== this.state.passengers.length) {
            APIService.getPassengers()
                .then((response) => {
                    this.setState({
                        passengers: response.data
                    });
                })
                .catch((err: Error) => {
                    // console.log(err);
                    alert(err);
                });
        }
    }

    handleClose = () => this.setState({ showAdd: false, showEdit: false, showAddFlight: false });
    handleShow = () => this.setState({ showAdd: true });
    editShow = (passenger: Passenger) => {
        this.setState({
            showEdit: true,
            passengerId: passenger.passengerId,
            name: passenger.name,
            job: passenger.job,
            email: passenger.email,
            age: passenger.age
        })
    };
    handleUpdates = (event: FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        let passenger: Passenger = {
            passengerId: this.state.passengerId,
            name: this.state.name,
            job: this.state.job,
            email: this.state.email,
            age: this.state.age,
        };
        APIService.updatePassenger(passenger)
            .then((response: AxiosResponse<Passenger>) => {
                let p = [...this.state.passengers];
                p = p.filter((item) => item.passengerId === passenger.passengerId);
                p.push(response.data);
                this.setState({
                    passengers: p
                });
            })
        this.handleClose();
    }
    handleClickPreventDefault = (e: MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        let passenger: Passenger = {
            passengerId: this.state.passengerId,
            name: this.state.name,
            job: this.state.job,
            email: this.state.email,
            age: this.state.age,
            //flightId: this.state.flightId
        };
        APIService.updatePassenger(passenger)
            .then((response: AxiosResponse<Passenger>) => {
                let p = [...this.state.passengers];
                p = p.filter((item) => item.passengerId === passenger.passengerId);
                p.push(response.data);
                this.setState({
                    passengers: p
                });
            })
        this.handleClose();
    }
    handleClickPreventDefault2 = (e: MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        this.handleAddPassengerToFlight(this.state.passengerId, this.state.flightId)
        this.handleClose();
    }
    handleFlightIdChange = (event: ChangeEvent<HTMLInputElement>) => this.setState({ flightId: event.target.value as unknown as number });
    handleNameChange = (event: ChangeEvent<HTMLInputElement>) => this.setState({ name: event.target.value });
    handleJobChange = (event: ChangeEvent<HTMLInputElement>) => this.setState({ job: event.target.value });
    handleEmailChange = (event: ChangeEvent<HTMLInputElement>) => this.setState({ email: event.target.value });
    handleAgeChange = (event: ChangeEvent<HTMLInputElement>) => this.setState({ age: event.target.value });
    handleDelete(passenger: Passenger) {
        APIService.deletePassenger(passenger)
            .then((response: AxiosResponse<Passenger>) => {
                let p = [...this.state.passengers];
                p.push(response.data);
                this.setState({
                    passengers: p
                });
            })
        this.forceUpdate();
    }
    addFlightShow = (passenger: Passenger) => {
        this.setState({
            showAddFlight: true,
            passengerId: passenger.passengerId,
            name: passenger.name,
            job: passenger.job,
            email: passenger.email,
            age: passenger.age,
        })
    };
    handleAddPassengerToFlight(passengerId: number, flightId: number) {
        APIService.addPassengerToFlight(passengerId, flightId)
    }
    handleSaveChanges = (event: FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        let passenger: AddPassenger = {
            name: this.state.name,
            job: this.state.job,
            email: this.state.email,
            age: this.state.age
        };

        APIService.createPassenger(passenger)
            .then((response: AxiosResponse<Passenger>) => {
                let p = [...this.state.passengers];
                p.push(response.data);
                this.setState({
                    passengers: p
                });
            })
        this.forceUpdate();
        this.handleClose();
    }
    render(): React.ReactNode {
        return (
            <div className="App container">
                <div className="jumbotron">
                    <h2>Passengers</h2>
                </div>
                <table className="table table-striped table-bordered table-hover table-highlight">
                    <thead>
                        <tr>
                            <th>Passenger Number</th>
                            <th>Name</th>
                            <th>Occupation</th>
                            <th>Email</th>
                            <th>Current Age</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.passengers.map((passenger: Passenger) => (
                            <React.Fragment key={passenger.passengerId}>
                                <tr id={"passenger-" + passenger.passengerId}>
                                    <td>{passenger.passengerId}</td>
                                    <td>{passenger.name}</td>
                                    <td>{passenger.job}</td>
                                    <td>{passenger.email}</td>
                                    <td>{passenger.age}</td>
                                    <td>
                                        <div className="btn-group">
                                            <Button variant="outline-primary" onClick={() => this.editShow(passenger)}>
                                                Edit
                                            </Button>
                                            <Button variant="outline-secondary" onClick={() => this.handleDelete(passenger)}>
                                                Delete
                                            </Button>
                                            <Button variant="outline-info" onClick={() => this.addFlightShow(passenger)}>
                                                Obtain Ticket
                                            </Button>
                                        </div>
                                    </td>
                                </tr>
                            </React.Fragment>
                        ))};
                    </tbody>
                </table>
                <>
                    <Button variant="outline-primary" onClick={this.handleShow}>
                        Add Passenger
                    </Button>

                    <Modal show={this.state.showAdd} onHide={this.handleClose}>
                        <Modal.Header closeButton>
                            <Modal.Title>Add Passenger</Modal.Title>
                        </Modal.Header>
                        <Modal.Body>
                            <Form onSubmit={this.handleSaveChanges}>
                                <Form.Group className="mb-3" controlId="passengerName">
                                    <Form.Label>Name</Form.Label>
                                    <Form.Control type="text" value={this.state.name} onChange={this.handleNameChange} required />
                                </Form.Group>
                                <Form.Group className="mb-3" controlId="passengerJob">
                                    <Form.Label>Job</Form.Label>
                                    <Form.Control type="text" value={this.state.job} onChange={this.handleJobChange} required />
                                </Form.Group>
                                <Form.Group className="mb-3" controlId="passengerEmail">
                                    <Form.Label>Email</Form.Label>
                                    <Form.Control type="text" value={this.state.email} onChange={this.handleEmailChange} required />
                                </Form.Group>
                                <Form.Group className="mb-3" controlId="passengerAge">
                                    <Form.Label>Age</Form.Label>
                                    <Form.Control type="text" pattern="[0-9]*" value={this.state.age} onChange={this.handleAgeChange} required />
                                </Form.Group>
                                <div className="btn-group">
                                    <Button variant="secondary" onClick={this.handleClose}>
                                        Close
                                    </Button>
                                    <Button variant="primary" type="submit">
                                        Save Changes
                                    </Button>
                                </div>
                            </Form>
                        </Modal.Body>
                    </Modal>
                    <Modal show={this.state.showAddFlight} onHide={this.handleClose}>
                        <Modal.Header closeButton>
                            <Modal.Title>Choose Flight</Modal.Title>
                        </Modal.Header>
                        <Modal.Body>
                            <Form onSubmit={this.handleUpdates}>
                                <Form.Group className="mb-3" controlId="flightId">
                                    <Form.Label>Flight Number</Form.Label>
                                    <Form.Control type="text" pattern="[0-9]*" value={this.state.flightId} onChange={this.handleFlightIdChange} required />
                                </Form.Group>
                                <div className="btn-group">
                                    <Button variant="secondary" onClick={this.handleClose}>
                                        Close
                                    </Button>
                                    <Button variant="primary" type="submit" onClick={this.handleClickPreventDefault2}>
                                        Confirm Ticket
                                    </Button>
                                </div>
                            </Form>
                        </Modal.Body>
                    </Modal>
                    <Modal show={this.state.showEdit} onHide={this.handleClose}>
                        <Modal.Header closeButton>
                            <Modal.Title>Edit {this.state.name}'s Information</Modal.Title>
                        </Modal.Header>
                        <Modal.Body>
                            <Form onSubmit={this.handleUpdates}>
                                <Form.Group className="mb-3" controlId="passengerName">
                                    <Form.Label>Name</Form.Label>
                                    <Form.Control type="text" value={this.state.name} onChange={this.handleNameChange} required />
                                </Form.Group>
                                <Form.Group className="mb-3" controlId="passengerJob">
                                    <Form.Label>Job</Form.Label>
                                    <Form.Control type="text" value={this.state.job} onChange={this.handleJobChange} required />
                                </Form.Group>
                                <Form.Group className="mb-3" controlId="passengerEmail">
                                    <Form.Label>Email</Form.Label>
                                    <Form.Control type="text" value={this.state.email} onChange={this.handleEmailChange} required />
                                </Form.Group>
                                <Form.Group className="mb-3" controlId="passengerAge">
                                    <Form.Label>Age</Form.Label>
                                    <Form.Control type="text" pattern="[0-9]*" value={this.state.age} onChange={this.handleAgeChange} required />
                                </Form.Group>
                                <div className="btn-group">
                                    <Button variant="secondary" onClick={this.handleClose}>
                                        Close
                                    </Button>
                                    <Button variant="primary" type="submit" onClick={this.handleClickPreventDefault}>
                                        Save Changes
                                    </Button>
                                </div>
                            </Form>
                        </Modal.Body>
                    </Modal>
                </>
            </div>
        );
    }

}

export default PassengersListView;