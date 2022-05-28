using System;

namespace tiszaszaki_asp_webapp_2022.Entities
{
    public class Todo
    {
		public long id { get; set; }
		public string name { get; set; }
		public string description { get; set; }
		public int phase { get; set; }
		public DateTime dateCreated { get; set; }
		public DateTime dateModified { get; set; }
		public DateTime deadline { get; set; }
		/*
		@Id
		@GeneratedValue(strategy = GenerationType.AUTO)
		@Column(updatable = false, nullable = false)
		private Long id;

		@NotBlank
		@Column(nullable = false, unique = true)
		private String name;

		@Size(max = TodoCommon.maxTodoDescriptionLength)
		@Column(length = TodoCommon.maxTodoDescriptionLength)
		private String description;

		@NotNull
		@Min(TodoCommon.phaseMin)
		@Max(TodoCommon.phaseMax)
		private Integer phase;

		@PastOrPresent
		@Column(nullable = false)
		@Temporal(TemporalType.TIMESTAMP)
		private Date dateCreated;

		@PastOrPresent
		@Column(nullable = false)
		@Temporal(TemporalType.TIMESTAMP)
		private Date dateModified;

		private Date deadline;
		@OneToMany(fetch = FetchType.EAGER, cascade = CascadeType.REMOVE, mappedBy = "todo")

		private List<Task> tasks;
		@ManyToOne
		@JoinColumn(name = "board_id", nullable = false)
		@JsonIgnore
		@ToString.Exclude

		private Board board;
		*/
	}
}
