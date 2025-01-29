namespace SCRSApplication.Models
{
    public class TeamsViewModel
    {
        public IEnumerable<TeamsEntity> TeamEntityList { get; set; }
        public IEnumerable<TeamMemberEntity> TeamMemberEntityList { get; set; }
        public TeamsEntity Teams { get; set; }
        public TeamMemberEntity TeamMember { get; set; }

      //   public ICollection<TeamMemberEntity> TMEntity { get; set; }

        //public TeamsViewModel()
        //{
        //    Teams = new TeamsEntity();
        //    TeamMember = new TeamMemberEntity();
        //}
    }
}
