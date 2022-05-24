import { AxiosResponse } from "axios";
import React, { ChangeEvent, FormEvent, MouseEvent } from "react";
import { Button, Form, Modal, Nav } from "react-bootstrap";
import Ticket, { AddTicket } from "../../models/ticket";
import APIService from "../../services/apiService";

type TicketListViewProps = {

}

type TicketListViewState = {
    showAdd: boolean;
    confirmationNumber: number;
    seatNumber: string;
    gateNumber: string;
    ticketClass: string;
    ticketPrice: string;
    passengerId: string;
    flightId: string;
    tickets: Ticket[];
    showEdit: boolean;
}

export class TicketListView extends React.Component<TicketListViewProps, TicketListViewState> {
    constructor(props: TicketListViewProps) {
        super(props);
        this.state = {
            showAdd: false,
            confirmationNumber: 0,
            seatNumber: "",
            gateNumber: "",
            ticketClass: "",
            ticketPrice: "",
            passengerId: "",
            flightId: "",
            tickets: [],
            showEdit: false
        }
    }
    componentDidMount() {
        APIService.getTickets()
            .then((response) => {
                this.setState({
                    tickets: response.data
                });
            })
            .catch((err: Error) => {
                //console.log(err);
                alert(err);
            });
    }
    componentDidUpdate(tProps: TicketListViewProps, pState: TicketListViewState) {
        if (pState.tickets.length !== this.state.tickets.length) {
            APIService.getTickets()
                .then((response) => {
                    this.setState({
                        tickets: response.data
                    });
                })
                .catch((err: Error) => {
                    //console.log(err);
                    alert(err);
                });
        }
    }

    handleClose = () => this.setState({ showAdd: false, showEdit: false });
    handleShow = () => this.setState({ showAdd: true });
    editShow = (ticket: Ticket) => {
        this.setState({
            showEdit: true,
            confirmationNumber: ticket.confirmationNumber,
            seatNumber: ticket.seatNumber,
            gateNumber: ticket.gateNumber,
            ticketClass: ticket.ticketClass,
            ticketPrice: ticket.ticketPrice,
            passengerId: ticket.passengerId,
            flightId: ticket.flightId
        })
    };
    handleUpdates = (event: FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        let ticket: Ticket = {
            confirmationNumber: this.state.confirmationNumber,
            seatNumber: this.state.seatNumber,
            gateNumber: this.state.gateNumber,
            ticketClass: this.state.ticketClass,
            ticketPrice: this.state.ticketPrice,
            passengerId: this.state.passengerId,
            flightId: this.state.flightId
        };
        APIService.updateTicket(ticket)
            .then((response: AxiosResponse<Ticket>) => {
                let t = [...this.state.tickets];
                t = t.filter((item) => item.confirmationNumber === ticket.confirmationNumber);
                t.push(response.data);
                this.setState({
                    tickets: t
                });
            })
        this.handleClose();
    }
    handleClickPreventDefault = (e: MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        let ticket: Ticket = {
            confirmationNumber: this.state.confirmationNumber,
            seatNumber: this.state.seatNumber,
            gateNumber: this.state.gateNumber,
            ticketClass: this.state.ticketClass,
            ticketPrice: this.state.ticketPrice,
            passengerId: this.state.passengerId,
            flightId: this.state.flightId
        };
        APIService.updateTicket(ticket)
            .then((response: AxiosResponse<Ticket>) => {
                let t = [...this.state.tickets];
                t = t.filter((item) => item.confirmationNumber === ticket.confirmationNumber);
                t.push(response.data);
                this.setState({
                    tickets: t
                });
            })
        this.handleClose();
    }
    handleticketClassChange = (event: ChangeEvent<HTMLInputElement>) => this.setState({ ticketClass: event.target.value });
    handleTicketPriceChange = (event: ChangeEvent<HTMLInputElement>) => this.setState({ ticketPrice: event.target.value });
    handlePassengerIdChange = (event: ChangeEvent<HTMLInputElement>) => this.setState({ passengerId: event.target.value });
    handleFlightIdChange = (event: ChangeEvent<HTMLInputElement>) => this.setState({ flightId: event.target.value });
    handleDelete(ticket: Ticket) {
        APIService.deleteTicket(ticket)
            .then((response: AxiosResponse<Ticket>) => {
                let t = [...this.state.tickets];
                t.push(response.data);
                this.setState({
                    tickets: t
                });
            })
        this.forceUpdate();
    }
    handleSaveChanges = (event: FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        let ticket: AddTicket = {
            ticketClass: this.state.ticketClass,
            ticketPrice: this.state.ticketPrice,
            passengerId: this.state.passengerId as unknown as number,
            flightId: this.state.flightId as unknown as number
        };

        APIService.createTicket(ticket)
            .then((response: AxiosResponse<Ticket>) => {
                let t = [...this.state.tickets];
                t.push(response.data);
                this.setState({
                    tickets: t
                });
            })
        this.forceUpdate();
        this.handleClose();
    }

    render(): React.ReactNode {
        return (
            <div className="App container">
                <div className="jumbotron">
                    <h2>Tickets</h2>
                </div>
                <table className="table table-striped table-bordered table-hover table-highlight">
                    <thead>
                        <tr>
                            <th>Confirmation Number</th>
                            <th>Passenger Id</th>
                            <th>Flight Id</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.tickets.map((ticket: Ticket) => (
                            <React.Fragment key={ticket.confirmationNumber}>
                                <tr id={"ticket-" + ticket.confirmationNumber}>
                                    <td>{ticket.confirmationNumber}</td>
                                    <td>{ticket.passengerId}</td>
                                    <td>{ticket.flightId}</td>
                                    <td>
                                        <div className="btn-group">
                                            <Button variant="outline-primary" onClick={() => this.editShow(ticket)}>
                                                Edit
                                            </Button>
                                            <Button variant="outline-secondary" onClick={() => this.handleDelete(ticket)}>
                                                Delete
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
                        Add Ticket
                    </Button>
                    <Modal show={this.state.showAdd} onHide={this.handleClose}>
                        <Modal.Header closeButton>
                            <Modal.Title>Add Ticket</ Modal.Title>
                        </Modal.Header>
                        <Modal.Body>
                            <Form onSubmit={this.handleSaveChanges}>
                                <Form.Group className="mb-3" controlId="passengerId">
                                    <Form.Label>Passenger Id</Form.Label>
                                    <Form.Control type="text" pattern="[0-9]*" value={this.state.passengerId} onChange={this.handlePassengerIdChange} required />
                                </Form.Group>
                                <Form.Group className="mb-3" controlId="flightId">
                                    <Form.Label>Flight Id</Form.Label>
                                    <Form.Control type="text" pattern="[0-9]*" value={this.state.flightId} onChange={this.handleFlightIdChange} required />
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

                    <Modal show={this.state.showEdit} onHide={this.handleClose}>
                        <Modal.Header closeButton>
                            <Modal.Title>Edit Ticket #{this.state.confirmationNumber}</Modal.Title>
                        </Modal.Header>
                        <Modal.Body>
                            <Form onSubmit={this.handleUpdates}>
                                <Form.Group className="mb-3" controlId="passengerId">
                                    <Form.Label>Passenger Id</Form.Label>
                                    <Form.Control type="text" pattern="[0-9]*" value={this.state.passengerId} onChange={this.handlePassengerIdChange} required/>
                                </Form.Group>
                                <Form.Group className="mb-3" controlId="flightId">
                                    <Form.Label>Flight Id</Form.Label>
                                    <Form.Control type="text" pattern="[0-9]*" value={this.state.flightId} onChange={this.handleFlightIdChange} required/>
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

export default TicketListView;